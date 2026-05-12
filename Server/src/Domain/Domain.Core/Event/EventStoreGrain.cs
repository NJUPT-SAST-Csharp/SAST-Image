using Domain.Database;
using Microsoft.EntityFrameworkCore;
using Orleans.Concurrency;

namespace Domain.Event;

[StatelessWorker]
internal sealed class EventStoreGrain(IDbContextFactory<DomainDbContext> factory)
    : Grain,
        IEventStoreGrain
{
    public async ValueTask<DomainEventUnit[]> GetEventsAsync(CancellationToken cancellationToken)
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);
        return await context
            .Events.AsNoTracking()
            .OrderBy(e => e.Timestamp)
            .ToArrayAsync(cancellationToken);
    }

    public async ValueTask<DomainEventUnit[]> GetEventsAsync(
        DateTime from,
        CancellationToken cancellationToken
    )
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);
        return await context
            .Events.AsNoTracking()
            .Where(e => e.Timestamp > from)
            .OrderBy(e => e.Timestamp)
            .ToArrayAsync(cancellationToken);
    }

    public async ValueTask<DomainEventUnit[]> GetEventsAsync(
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken
    )
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);
        return await context
            .Events.AsNoTracking()
            .Where(e => e.Timestamp > from && e.Timestamp <= to)
            .OrderBy(e => e.Timestamp)
            .ToArrayAsync(cancellationToken);
    }
}
