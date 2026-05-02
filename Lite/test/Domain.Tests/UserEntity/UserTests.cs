using System.Buffers.Binary;
using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Domain.Shared;
using Domain.UserAggregate;
using Domain.UserAggregate.Commands;
using Domain.UserAggregate.Commands.Profile;
using Domain.UserAggregate.Events;
using Domain.UserAggregate.Exceptions;
using Domain.UserAggregate.Services;
using Domain.UserAggregate.UserEntity;
using Moq;
using Shouldly;

namespace Domain.Tests.UserEntity;

[TestClass]
public sealed class UserTests(TestContext context)
{
    #region Register

    [TestMethod]
    public async Task Add_User_And_Raise_Event_When_Register()
    {
        List<User> db = [];
        RegisterCommand command = new(
            Username.New(),
            Nickname.New(),
            Email.New,
            PasswordInput.New(),
            RegistryCode.New
        );
        var usernameChecker = new Mock<IUsernameUniquenessChecker>();
        var codeChecker = new Mock<IRegistryCodeChecker>();
        var pwdGenerator = new Mock<IPasswordGenerator>();
        var jwtGenerator = new Mock<IJwtTokenGenerator>();
        var repository = new Mock<IUserRepository>();
        var password = Password.New;
        var token = JwtToken.New;

        usernameChecker
            .Setup(c => c.CheckAsync(command.Username, context.CancellationToken))
            .Returns(Task.CompletedTask);
        codeChecker
            .Setup(c => c.CheckAsync(command.Email, command.Code, context.CancellationToken))
            .Returns(Task.CompletedTask);
        pwdGenerator
            .Setup(g => g.GenerateAsync(command.Password, context.CancellationToken))
            .ReturnsAsync(password);
        jwtGenerator
            .Setup(g => g.Generate(It.IsAny<UserId>(), command.Username, It.IsAny<Roles>()))
            .Returns(token);
        repository
            .Setup(r => r.AddAsync(It.IsAny<User>(), context.CancellationToken))
            .Callback<User, CancellationToken>((user, _) => db.Add(user))
            .Returns(Task.CompletedTask);

        var result = await User.RegisterAsync(
            command,
            usernameChecker.Object,
            codeChecker.Object,
            pwdGenerator.Object,
            jwtGenerator.Object,
            repository.Object,
            context.CancellationToken
        );

        result.ShouldBe(token);
        db.Count.ShouldBe(1);
        db[0].DomainEvents.Count.ShouldBe(1);
        db[0].DomainEvents.First().ShouldBeOfType<UserRegisteredEvent>();
        usernameChecker.Verify(
            c => c.CheckAsync(command.Username, context.CancellationToken),
            Times.Once
        );
        codeChecker.Verify(
            c => c.CheckAsync(command.Email, command.Code, context.CancellationToken),
            Times.Once
        );
        pwdGenerator.Verify(
            g => g.GenerateAsync(command.Password, context.CancellationToken),
            Times.Once
        );
        jwtGenerator.Verify(
            g => g.Generate(It.IsAny<UserId>(), command.Username, It.IsAny<Roles>()),
            Times.Once
        );
        repository.Verify(r => r.AddAsync(It.IsAny<User>(), context.CancellationToken), Times.Once);
    }

    #endregion

    #region Login

    [TestMethod]
    public async Task Return_Token_When_Login_Success()
    {
        var user = User.New(
            id: 123,
            username: Username.New(),
            password: Password.New,
            refreshToken: RefreshToken.New()
        );
        LoginCommand command = new(Username.New(), PasswordInput.New("password2"));
        var validator = new Mock<IPasswordValidator>();
        var jwtGenerator = new Mock<IJwtTokenGenerator>();
        var expectedToken = JwtToken.New;

        validator
            .Setup(v =>
                v.ValidateAsync(It.IsAny<Password>(), command.Password, context.CancellationToken)
            )
            .Returns(Task.CompletedTask);
        jwtGenerator
            .Setup(g => g.Generate(user.Id, It.IsAny<Username>(), It.IsAny<Roles>()))
            .Returns(expectedToken);

        var actualToken = await user.LoginAsync(
            command,
            validator.Object,
            jwtGenerator.Object,
            context.CancellationToken
        );

        actualToken.ShouldBe(expectedToken);
        validator.Verify(
            v => v.ValidateAsync(It.IsAny<Password>(), command.Password, context.CancellationToken),
            Times.Once
        );
        jwtGenerator.Verify(
            g => g.Generate(user.Id, It.IsAny<Username>(), It.IsAny<Roles>()),
            Times.Once
        );
    }

    #endregion

    #region ResetPassword

    [TestMethod]
    public async Task Set_New_Password_When_ResetPassword()
    {
        var oldPassword = Password.New;
        var newPassword = new Password([7, 8, 9], [4, 5, 6]);
        var user = User.New(id: 123, username: Username.New(), password: oldPassword);
        ResetPasswordCommand command = new(
            PasswordInput.New("password1"),
            PasswordInput.New("password2"),
            Actor.Author
        );
        var validator = new Mock<IPasswordValidator>();
        var generator = new Mock<IPasswordGenerator>();

        validator
            .Setup(v =>
                v.ValidateAsync(oldPassword, command.OldPassword, context.CancellationToken)
            )
            .Returns(Task.CompletedTask);
        generator
            .Setup(g => g.GenerateAsync(command.NewPassword, context.CancellationToken))
            .ReturnsAsync(newPassword);

        await user.ResetPasswordAsync(
            command,
            validator.Object,
            generator.Object,
            context.CancellationToken
        );

        user.Password.ShouldBe(newPassword);
        validator.Verify(
            v => v.ValidateAsync(oldPassword, command.OldPassword, context.CancellationToken),
            Times.Once
        );
        generator.Verify(
            g => g.GenerateAsync(command.NewPassword, context.CancellationToken),
            Times.Once
        );
    }

    #endregion

    #region RefreshToken

    [TestMethod]
    public void Throw_When_RefreshToken_Not_Initialized()
    {
        var user = User.New(
            id: 123,
            username: Username.New(),
            password: Password.New,
            refreshToken: default
        );
        RefreshTokenCommand command = new(RefreshToken.New());
        var jwtGenerator = new Mock<IJwtTokenGenerator>();

        Should.Throw<RefreshTokenInvalidException>(() =>
            user.RefreshToken(command, jwtGenerator.Object)
        );
    }

    [TestMethod]
    public void Throw_When_RefreshToken_Not_Match()
    {
        var user = User.New(
            id: 123,
            username: Username.New(),
            password: Password.New,
            refreshToken: RefreshToken.New(userId: 123, expiresAt: DateTime.UtcNow.AddMinutes(5))
        );
        RefreshTokenCommand command = new(
            RefreshToken.New(userId: 123, expiresAt: DateTime.UtcNow.AddMinutes(10))
        );
        var jwtGenerator = new Mock<IJwtTokenGenerator>();

        Should.Throw<RefreshTokenInvalidException>(() =>
            user.RefreshToken(command, jwtGenerator.Object)
        );
    }

    [TestMethod]
    public void Return_New_Token_And_Rotate_RefreshToken_When_Refresh_Success()
    {
        var oldRefresh = RefreshToken.New(userId: 123, expiresAt: DateTime.UtcNow.AddMinutes(5));
        var newRefresh1 = RefreshToken.New(userId: 123, expiresAt: DateTime.UtcNow.AddMinutes(10));
        var newRefresh2 = RefreshToken.New(userId: 123, expiresAt: DateTime.UtcNow.AddMinutes(15));
        var user = User.New(
            id: 123,
            username: Username.New(),
            password: Password.New,
            refreshToken: oldRefresh
        );
        var jwtGenerator = new Mock<IJwtTokenGenerator>();

        jwtGenerator
            .SetupSequence(g => g.Generate(user.Id, It.IsAny<Username>(), It.IsAny<Roles>()))
            .Returns(new JwtToken("a1", newRefresh1, 100))
            .Returns(new JwtToken("a2", newRefresh2, 100));

        var token1 = user.RefreshToken(new(oldRefresh), jwtGenerator.Object);

        token1.RefreshToken.ShouldBe(newRefresh1);
        Should.Throw<RefreshTokenInvalidException>(() =>
            user.RefreshToken(new(oldRefresh), jwtGenerator.Object)
        );

        var token2 = user.RefreshToken(new(newRefresh1), jwtGenerator.Object);

        token2.RefreshToken.ShouldBe(newRefresh2);
    }

    #endregion

    #region ResetUsername

    [TestMethod]
    public void Raise_Event_When_ResetUsername()
    {
        var user = User.New(id: 123, username: Username.New("old_user"), password: Password.New);
        var username = Username.New("new_user");
        ResetUsernameCommand command = new(username, Actor.Author);

        user.ResetUsername(command);

        user.Username.ShouldBe(username);
        user.DomainEvents.Count.ShouldBe(1);
        user.DomainEvents.First().ShouldBeOfType<UsernameResetEvent>();
    }

    #endregion

    #region UpdateProfile

    [TestMethod]
    public void Raise_Event_When_UpdateProfile()
    {
        var user = User.New(id: 123, username: Username.New(), password: Password.New);
        UpdateProfileCommand command = new(
            Nickname.New("new_nickname"),
            Biography.New("new_biography"),
            Actor.Author
        );

        user.UpdateProfile(command);

        user.DomainEvents.Count.ShouldBe(1);
        user.DomainEvents.First().ShouldBeOfType<ProfileUpdatedEvent>();
    }

    #endregion

    #region UpdateAvatar

    [TestMethod]
    public void Raise_Event_When_UpdateAvatar()
    {
        var user = User.New(id: 123, username: Username.New(), password: Password.New);
        UpdateAvatarCommand command = new(ImageFile.New, Actor.Author);

        user.UpdateAvatar(command);

        user.DomainEvents.Count.ShouldBe(1);
        user.DomainEvents.First().ShouldBeOfType<AvatarUpdatedEvent>();
    }

    #endregion

    #region UpdateHeader

    [TestMethod]
    public void Raise_Event_When_UpdateHeader()
    {
        var user = User.New(id: 123, username: Username.New(), password: Password.New);
        UpdateHeaderCommand command = new(ImageFile.New, Actor.Author);

        user.UpdateHeader(command);

        user.DomainEvents.Count.ShouldBe(1);
        user.DomainEvents.First().ShouldBeOfType<HeaderUpdatedEvent>();
    }

    #endregion
}

internal static class TestUser
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    private static extern User Constructor();

    extension(User user)
    {
        private T GetValue<T>() => user.GetValue<User, T>();

        public static User New(
            UserId id,
            Username username,
            Password password,
            RefreshToken refreshToken = default,
            Role[]? roles = null
        )
        {
            var u = Constructor();

            u.SetId(id);
            u.SetValue("username", username);
            u.SetValue("password", password);
            u.SetValue("refreshToken", refreshToken);
            u.SetValue("roles", Roles.New([Role.User]));

            return u;
        }

        public Password Password => user.GetValue<Password>();

        public Username Username => user.GetValue<Username>();
    }
}

file static class UsernameTestHelper
{
    extension(Username)
    {
        public static Username New(string value = "user_123")
        {
            Username.TryCreateNew(value, out var username).ShouldBeTrue();
            return username;
        }
    }
}

file static class NicknameTestHelper
{
    extension(Nickname)
    {
        public static Nickname New(string value = "nickname")
        {
            Nickname.TryCreateNew(value, out var nickname).ShouldBeTrue();
            return nickname;
        }
    }
}

file static class RolesTestHelper
{
    extension(Roles)
    {
        public static Roles New(params Role[] roles) => new(roles);
    }
}

file static class BiographyTestHelper
{
    extension(Biography)
    {
        public static Biography New(string value = "biography")
        {
            Biography.TryCreateNew(value, out var biography).ShouldBeTrue();
            return biography;
        }
    }
}

file static class PasswordInputTestHelper
{
    extension(PasswordInput)
    {
        public static PasswordInput New(string value = "password1")
        {
            PasswordInput.TryCreateNew(value, out var password).ShouldBeTrue();
            return password;
        }
    }
}

file static class RegistryCodeTestHelper
{
    extension(RegistryCode)
    {
        public static RegistryCode New => new(123456);
    }
}

file static class PasswordTestHelper
{
    extension(Password)
    {
        public static Password New => new([1, 2, 3], [4, 5, 6]);
    }
}

file static class RefreshTokenTestHelper
{
    extension(RefreshToken)
    {
        public static RefreshToken New(long userId = 123, DateTime? expiresAt = null)
        {
            Span<byte> buffer = stackalloc byte[RefreshToken.ByteLength];
            BinaryPrimitives.WriteInt64LittleEndian(buffer[0..8], userId);
            BinaryPrimitives.WriteInt64LittleEndian(
                buffer[8..16],
                (expiresAt ?? DateTime.UtcNow.AddMinutes(5)).ToBinary()
            );
            RandomNumberGenerator.Fill(buffer[16..]);

            RefreshToken
                .TryCreateNew(Base64Url.EncodeToString(buffer), out var refreshToken)
                .ShouldBeTrue();

            return refreshToken;
        }
    }
}

file static class JwtTokenTestHelper
{
    extension(JwtToken)
    {
        public static JwtToken New => new("access", RefreshToken.New(), 3600);
    }
}

file static class EmailTestHelper
{
    extension(Email)
    {
        public static Email New => new("123456@example.com");
    }
}

file static class ImageFileTestHelper
{
    extension(ImageFile)
    {
        public static ImageFile New => default;
    }
}
