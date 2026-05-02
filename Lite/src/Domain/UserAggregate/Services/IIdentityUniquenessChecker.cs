using Domain.UserAggregate.IdentityEntity;

namespace Domain.UserAggregate.Services;

public interface IIdentityUniquenessChecker
{
    public Task CheckAsync(
        IdentityId identityId,
        IdentityProvider provider,
        CancellationToken cancellationToken
    );
}
