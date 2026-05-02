using Domain.UserAggregate.Exceptions;
using Domain.UserAggregate.UserEntity;
using Microsoft.Extensions.Caching.Distributed;

namespace Domain.UserAggregate.Services;

internal sealed class RegistryCodeChecker(IDistributedCache cache) : IRegistryCodeChecker
{
    public async Task CheckAsync(
        Email email,
        RegistryCode code,
        CancellationToken cancellationToken
    )
    {
        string? value = await cache.GetStringAsync(code.Value.ToString(), cancellationToken);

        if (long.TryParse(value, out long stored) is false || stored != code.Value)
        {
            throw new RegistryCodeException();
        }
    }
}
