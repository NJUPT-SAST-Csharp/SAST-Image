using Domain.Event;
using Domain.UserAggregate.Events;
using Storage.Database;
using Storage.Users.Messages;

namespace Storage.Users.EventHandlers;

public sealed class AvatarUpdatedEventHandler(StorageDbContext context)
    : IDomainEventHandler<AvatarUpdatedEvent>
{
    public async ValueTask Handle(AvatarUpdatedEvent e, CancellationToken cancellationToken)
    {
        await context.Messages.AddAsync(
            new AvatarUpdatedMessage(DateTime.UtcNow, e.Avatar, e.User),
            cancellationToken
        );
    }
}
