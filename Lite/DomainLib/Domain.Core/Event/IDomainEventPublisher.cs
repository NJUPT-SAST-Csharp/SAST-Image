namespace Domain.Event;

public interface IDomainEventPublisher
{
    public Task PublishAsync<TEvent>(
        TEvent domainEvent,
        CancellationToken cancellationToken = default
    )
        where TEvent : IDomainEvent;
}
