using Domain.Event;
using Domain.User.Events;

namespace Query.Users.EventHandlers;

public sealed class HeaderUpdatedEventHandler : IDomainEventHandler<HeaderUpdatedEvent>
{
    public ValueTask Handle(HeaderUpdatedEvent request, CancellationToken cancellationToken)
    {
        //Silence.
        return ValueTask.CompletedTask;
    }
}
