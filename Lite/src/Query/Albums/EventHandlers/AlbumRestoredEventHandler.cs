using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumRestoredEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumRestoredEvent>
{
    public async ValueTask Handle(AlbumRestoredEvent e, CancellationToken cancellationToken)
    {
        var album = await context.Albums.GetAsync(
            album => album.Id == e.Album.Value,
            cancellationToken
        );

        album.Restore(e);
    }
}
