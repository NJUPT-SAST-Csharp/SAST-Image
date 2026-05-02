using Domain.UserAggregate.UserEntity;

namespace Domain.UserAggregate.Services;

internal class RegistryCodeEmailClient : IRegistryCodeEmailClient
{
    public Task SendAsync(Email email, RegistryCode code, CancellationToken cancellationToken)
    {
        // TODO: Implement email sending logic here, e.g., using an SMTP client or an email service API.
        throw new NotImplementedException();
    }
}
