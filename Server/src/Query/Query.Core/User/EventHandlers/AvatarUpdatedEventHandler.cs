using Domain.Event;
using Domain.User.Events;
using Mediator;

namespace Query.User.EventHandlers;

public sealed class AvatarUpdatedEventHandler : IDomainEventHandler<AvatarUpdatedEvent>
{
    public ValueTask<Unit> Handle(AvatarUpdatedEvent request, CancellationToken cancellationToken)
    {
        // Silence.
        return Unit.ValueTask;
    }
}
