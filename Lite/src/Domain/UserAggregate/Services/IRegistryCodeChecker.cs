using Domain.UserAggregate.UserEntity;

namespace Domain.UserAggregate.Services;

public interface IRegistryCodeChecker
{
    public Task CheckAsync(Email email, RegistryCode code, CancellationToken cancellationToken);
}
