using Mediator;

namespace Domain.Event;

public sealed class EventPublisher(IMediator publisher) : IDomainEventPublisher
{
    public async Task PublishAsync<TEvent>(
        TEvent domainEvent,
        CancellationToken cancellationToken = default
    )
        where TEvent : IDomainEvent
    {
        await publisher.Publish(domainEvent, cancellationToken);
    }
}
