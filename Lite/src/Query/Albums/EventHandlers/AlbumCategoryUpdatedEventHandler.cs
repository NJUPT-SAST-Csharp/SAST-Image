using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumCategoryUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumCategoryUpdatedEvent>
{
    public async ValueTask Handle(AlbumCategoryUpdatedEvent e, CancellationToken cancellationToken)
    {
        var album = await context.Albums.GetAsync(
            album => album.Id == e.Album.Value,
            cancellationToken
        );

        album.CategoryId = e.Category.Value;
    }
}
