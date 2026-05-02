using Domain.Entity;
using Domain.UserAggregate.Commands;
using Domain.UserAggregate.Commands.OAuth.GitHub;
using Domain.UserAggregate.Commands.Profile;
using Domain.UserAggregate.Events;
using Domain.UserAggregate.Exceptions;
using Domain.UserAggregate.IdentityEntity;
using Domain.UserAggregate.Services;

namespace Domain.UserAggregate.UserEntity;

/// <summary>
/// Represents an authenticated user within the system.
/// </summary>
/// <remarks>
/// The User class encapsulates user identity, authentication credentials, and profile-related actions. It supports both
/// standard and external authentication flows, as well as operations for updating user profile information. Instances
/// of this class are typically managed through repository and service abstractions. This class is sealed and cannot be
/// inherited.
/// </remarks>
public sealed class User : EntityBase<UserId>
{
    [Obsolete("For ORM", true)]
    private User()
        : base(default) { }

    private Username _username;
    private Password _password;
    private RefreshToken _refreshToken;
    private Email _email; // TODO: ?
    private readonly Roles _roles = [];
    private readonly List<Identity> _identities = [];

    private User(Username username, Password password, Email email)
        : base(UserId.GenerateNew())
    {
        _email = email;
        _username = username;
        _password = password;
        _roles = new(Role.User);
    }

    #region Auth

    internal static async Task<JwtToken> RegisterAsync(
        RegisterCommand command,
        IUsernameUniquenessChecker usernameChecker,
        IRegistryCodeChecker codeChecker,
        IPasswordGenerator pwdGenerator,
        IJwtTokenGenerator jwtGenerator,
        IUserRepository repository,
        CancellationToken cancellationToken
    )
    {
        await Task.WhenAll(
            usernameChecker.CheckAsync(command.Username, cancellationToken),
            codeChecker.CheckAsync(command.Email, command.Code, cancellationToken)
        );

        var password = await pwdGenerator.GenerateAsync(command.Password, cancellationToken);

        var user = new User(command.Username, password, command.Email);

        var token = jwtGenerator.Generate(user.Id, user._username, user._roles);
        user._refreshToken = token.RefreshToken;
        await repository.AddAsync(user, cancellationToken);

        user.AddDomainEvent(new UserRegisteredEvent(user.Id, command.Username, command.Nickname));

        return token;
    }

    public async Task<JwtToken> LoginAsync(
        LoginCommand command,
        IPasswordValidator validator,
        IJwtTokenGenerator generator,
        CancellationToken cancellationToken
    )
    {
        await validator.ValidateAsync(_password, command.Password, cancellationToken);

        var token = generator.Generate(Id, _username, _roles);

        _refreshToken = token.RefreshToken;

        return token;
    }

    public async Task ResetPasswordAsync(
        ResetPasswordCommand command,
        IPasswordValidator validator,
        IPasswordGenerator generator,
        CancellationToken cancellationToken
    )
    {
        await validator.ValidateAsync(_password, command.OldPassword, cancellationToken);

        _password = await generator.GenerateAsync(command.NewPassword, cancellationToken);
    }

    public JwtToken RefreshToken(RefreshTokenCommand command, IJwtTokenGenerator generator)
    {
        if (_refreshToken == default || _refreshToken != command.RefreshToken)
            throw new RefreshTokenInvalidException();

        var token = generator.Generate(Id, _username, _roles);
        _refreshToken = token.RefreshToken;
        return token;
    }

    public void ResetUsername(ResetUsernameCommand command)
    {
        _username = command.Username;

        AddDomainEvent(new UsernameResetEvent(Id, _username));
    }

    #endregion

    #region Profile


    public void UpdateProfile(UpdateProfileCommand command)
    {
        if (command.Nickname is null && command.Biography is null)
            return;

        AddDomainEvent(new ProfileUpdatedEvent(Id, command.Nickname, command.Biography));
    }

    public void UpdateAvatar(UpdateAvatarCommand command)
    {
        AddDomainEvent(new AvatarUpdatedEvent(Id, command.Avatar));
    }

    public void UpdateHeader(UpdateHeaderCommand command)
    {
        AddDomainEvent(new HeaderUpdatedEvent(Id, command.Header));
    }

    #endregion

    #region GitHub OAuth

    public JwtToken Login(GitHubLoginCommand _, IJwtTokenGenerator generator)
    {
        var token = generator.Generate(Id, _username, _roles);
        _refreshToken = token.RefreshToken;
        return token;
    }

    /// <summary>
    /// Links the specified GitHub account to the current entity after verifying its uniqueness.
    /// </summary>
    /// <param name="command">The command containing the GitHub account identifier to link. Cannot be null.</param>
    /// <param name="checker">
    /// The uniqueness checker used to ensure the GitHub account is not already linked. Cannot be null.
    /// </param>
    /// <exception cref="IdentityDuplicateException">
    /// thrown if the GitHub account is already linked to another user.
    /// </exception>
    public async Task LinkAsync(
        GitHubLinkCommand command,
        IIdentityUniquenessChecker checker,
        CancellationToken cancellationToken
    )
    {
        await checker.CheckAsync(command.GitHubId, IdentityProvider.GitHub, cancellationToken);

        _identities.LinkGitHub(command.GitHubId);
    }

    #endregion
}
