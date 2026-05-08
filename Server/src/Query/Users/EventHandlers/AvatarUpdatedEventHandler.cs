using Domain.Event;
using Domain.User.Events;

namespace Query.Users.EventHandlers;

public sealed class AvatarUpdatedEventHandler : IDomainEventHandler<AvatarUpdatedEvent>
{
    public ValueTask Handle(AvatarUpdatedEvent request, CancellationToken cancellationToken)
    {
        // Silence.
        return ValueTask.CompletedTask;
    }
}
