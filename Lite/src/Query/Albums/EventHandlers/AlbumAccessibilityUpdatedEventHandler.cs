using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumAccessLevelUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumAccessLevelUpdatedEvent>
{
    public async ValueTask Handle(
        AlbumAccessLevelUpdatedEvent e,
        CancellationToken cancellationToken
    )
    {
        var album = await context.Albums.GetAsync(
            album => album.Id == e.Album.Value,
            cancellationToken
        );

        album.UpdateAccessLevel(e);
    }
}
