namespace Domain.Event;

[Alias("EventStoreGrain")]
public interface IEventStoreGrain : IGrainWithGuidKey
{
    [Alias("GetEvents")]
    [return: Immutable]
    ValueTask<DomainEventUnit[]> GetEventsAsync(CancellationToken cancellationToken);

    [Alias("GetEventsFrom")]
    [return: Immutable]
    ValueTask<DomainEventUnit[]> GetEventsAsync(DateTime from, CancellationToken cancellationToken);

    [Alias("GetEventsFromTo")]
    [return: Immutable]
    ValueTask<DomainEventUnit[]> GetEventsAsync(
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken
    );
}
