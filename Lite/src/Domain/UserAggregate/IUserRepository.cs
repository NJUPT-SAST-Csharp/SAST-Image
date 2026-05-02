using Domain.UserAggregate.IdentityEntity;
using Domain.UserAggregate.UserEntity;

namespace Domain.UserAggregate;

internal interface IUserRepository
{
    public Task AddAsync(User user, CancellationToken cancellationToken);
    public Task<User> GetAsync(UserId id, CancellationToken cancellationToken);
    public Task<User?> GetOrDefaultAsync(Username username, CancellationToken cancellationToken);
    public Task<User?> GetOrDefaultAsync(
        IdentityId externalId,
        CancellationToken cancellationToken
    );
}
