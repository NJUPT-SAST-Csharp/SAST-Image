using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumSubscribedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumSubscribedEvent>
{
    public async ValueTask Handle(AlbumSubscribedEvent e, CancellationToken cancellationToken)
    {
        SubscribeModel subscribe = new(e.Album.Value, e.User.Value);

        await context.Subscribes.AddAsync(subscribe, cancellationToken);
    }
}
