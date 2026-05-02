using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumRemovedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumRemovedEvent>
{
    public async ValueTask Handle(AlbumRemovedEvent e, CancellationToken cancellationToken)
    {
        var album = await context.Albums.GetAsync(
            album => album.Id == e.Album.Value,
            cancellationToken
        );

        album.Remove(e);
    }
}
