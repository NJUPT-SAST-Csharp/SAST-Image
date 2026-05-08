using Domain.Filters;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services;

public sealed class IdUniquenessChecker(IDbContextFactory<DomainDbContext> factory)
    : IIdUniquenessChecker
{
    public async ValueTask<bool> ExistsAsync(long id, CancellationToken cancellationToken = default)
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);
        return await context.Events.AnyAsync(s => s.GrainId == id, cancellationToken);
    }
}
