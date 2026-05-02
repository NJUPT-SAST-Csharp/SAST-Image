using Domain.UserAggregate.UserEntity;

namespace Domain.UserAggregate.Services;

public interface IRegistryCodeEmailClient
{
    public Task SendAsync(Email email, RegistryCode code, CancellationToken cancellationToken);
}
