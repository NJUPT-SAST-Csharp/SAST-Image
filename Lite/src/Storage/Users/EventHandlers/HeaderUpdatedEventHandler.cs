using Domain.Event;
using Domain.UserAggregate.Events;
using Storage.Database;
using Storage.Users.Messages;

namespace Storage.Users.EventHandlers;

public sealed class HeaderUpdatedEventHandler(StorageDbContext context)
    : IDomainEventHandler<HeaderUpdatedEvent>
{
    public async ValueTask Handle(HeaderUpdatedEvent e, CancellationToken cancellationToken)
    {
        await context.Messages.AddAsync(
            new HeaderUpdatedMessage(DateTime.UtcNow, e.Header, e.User),
            cancellationToken
        );
    }
}
