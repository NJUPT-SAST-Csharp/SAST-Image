using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services;

internal sealed class UsernameUniquenessChecker(IDbContextFactory<DomainDbContext> factory)
    : IUsernameUniquenessChecker
{
    public async ValueTask<bool> ExistsAsync(
        Username username,
        CancellationToken cancellationToken = default
    )
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);

        // NOTE: This is a bit hacky, but it works (maybe). We need to check if there is any snapshot that contains the username in its JSON value.
        return await context
            .Snapshots.Where(s =>
                EF.Functions.JsonContains(
                    s.Value,
                    $@"{{""{nameof(UserState.Username)}"": ""{username.Value}""}}"
                )
            )
            .AnyAsync(cancellationToken);
    }
}
