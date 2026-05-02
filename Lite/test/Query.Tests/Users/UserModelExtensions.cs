using System.Runtime.CompilerServices;

namespace Query.Users;

internal static class UserModelExtensions
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    private static extern UserModel _Create();

    extension(UserModel)
    {
        public static UserModel Create(
            long id,
            string username,
            string nickname,
            string biography,
            DateTime registeredAt
        )
        {
            var user = _Create();

            user.Set(a => a.Id, id);
            user.Set(a => a.Username, username);
            user.Set(a => a.Nickname, nickname);
            user.Set(a => a.Biography, biography);
            user.Set(a => a.RegisteredAt, registeredAt);

            return user;
        }
    }
}
