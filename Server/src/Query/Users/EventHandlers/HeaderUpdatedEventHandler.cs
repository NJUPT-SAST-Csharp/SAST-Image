using Domain.Event;
using Domain.User.Events;
using Mediator;

namespace Query.Users.EventHandlers;

public sealed class HeaderUpdatedEventHandler : IDomainEventHandler<HeaderUpdatedEvent>
{
    public ValueTask<Unit> Handle(HeaderUpdatedEvent request, CancellationToken cancellationToken)
    {
        //Silence.
        return Unit.ValueTask;
    }
}
