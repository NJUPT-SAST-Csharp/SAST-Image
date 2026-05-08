global using UserId = long;
using Domain.Filters;

namespace Domain.User;

[Alias("user_grain")]
public interface IUserGrain : IGrainWithIntegerKey
{
    [EnsureUniqueId]
    [AllowRecordNotExists]
    [Alias("user_register")]
    ValueTask<long> Register(Username username, Nickname Nickname, Biography Biography);

    [AccessControl]
    [Alias("user_update_profile")]
    ValueTask UpdateProfile(Username? username, Nickname? nickname, Biography? biography);

    [AccessControl]
    [Alias("user_update_avatar")]
    ValueTask UpdateAvatar(ImageFile file);

    [AccessControl]
    [Alias("user_update_header")]
    ValueTask UpdateHeader(ImageFile file);
}
