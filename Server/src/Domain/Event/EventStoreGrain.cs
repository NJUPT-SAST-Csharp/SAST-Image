using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Orleans.Concurrency;

namespace Domain.Event;

[StatelessWorker]
internal sealed class EventStoreGrain(IDbContextFactory<DomainDbContext> factory)
    : Grain,
        IEventStoreGrain
{
    private static readonly ConcurrentBag<string> connectionTargets = [];
    private static readonly Lock registerLock = new();

    [ReadOnly]
    public async IAsyncEnumerable<DomainEventUnit> GetEventsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);
        var events = context.Events.AsNoTracking().OrderBy(e => e.Timestamp).AsAsyncEnumerable();
        await foreach (var @event in events.WithCancellation(cancellationToken))
            yield return @event;
    }

    [ReadOnly]
    public async IAsyncEnumerable<DomainEventUnit> GetEventsAsync(
        DateTime from,
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);
        var events = context
            .Events.AsNoTracking()
            .Where(e => e.Timestamp > from)
            .OrderBy(e => e.Timestamp)
            .AsAsyncEnumerable();
        await foreach (var @event in events.WithCancellation(cancellationToken))
            yield return @event;
    }

    [ReadOnly]
    public async IAsyncEnumerable<DomainEventUnit> GetEventsAsync(
        DateTime from,
        DateTime to,
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);
        var events = context
            .Events.AsNoTracking()
            .Where(e => e.Timestamp > from && e.Timestamp <= to)
            .OrderBy(e => e.Timestamp)
            .AsAsyncEnumerable();
        await foreach (var @event in events.WithCancellation(cancellationToken))
            yield return @event;
    }
}
