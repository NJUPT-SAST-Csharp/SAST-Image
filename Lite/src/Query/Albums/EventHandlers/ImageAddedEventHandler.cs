using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class ImageAddedEventHandler(QueryDbContext context)
    : IDomainEventHandler<ImageAddedEvent>
{
    public async ValueTask Handle(ImageAddedEvent e, CancellationToken cancellationToken)
    {
        var album = await context.Albums.GetAsync(
            album => album.Id == e.Album.Value,
            cancellationToken
        );

        album.UpdatedAt = e.CreatedAt;
    }
}
