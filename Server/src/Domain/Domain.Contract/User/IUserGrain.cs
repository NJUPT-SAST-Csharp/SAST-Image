using Domain.Filters;
using Orleans.Concurrency;

namespace Domain.User;

[Alias("user_grain")]
public interface IUserGrain : IGrainWithIntegerKey
{
    [EnsureUniqueId]
    [AllowRecordNotExists]
    [Alias("user_register")]
    ValueTask<UserId> Register(Username username, Nickname Nickname, Biography Biography);

    [AccessControl]
    [Alias("user_update_profile")]
    ValueTask UpdateProfile(Username? username, Nickname? nickname, Biography? biography);

    [AccessControl]
    [Alias("user_update_avatar")]
    ValueTask UpdateAvatar(Immutable<byte[]> file, CancellationToken cancellationToken = default);

    [AccessControl]
    [Alias("user_update_header")]
    ValueTask UpdateHeader(Immutable<byte[]> file, CancellationToken cancellationToken = default);
}
