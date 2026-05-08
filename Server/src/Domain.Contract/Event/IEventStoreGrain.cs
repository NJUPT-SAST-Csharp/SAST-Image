namespace Domain.Event;

[Alias("event_store")]
public interface IEventStoreGrain : IGrainWithGuidKey
{
    [Alias("get_events")]
    IAsyncEnumerable<DomainEventUnit> GetEventsAsync(CancellationToken cancellationToken);

    [Alias("get_events_from")]
    IAsyncEnumerable<DomainEventUnit> GetEventsAsync(
        DateTime from,
        CancellationToken cancellationToken
    );

    [Alias("get_events_from_to")]
    IAsyncEnumerable<DomainEventUnit> GetEventsAsync(
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken
    );
}
