using Domain.Album.Events;
using Domain.Event;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumUpdatedEvent>
{
    public async ValueTask Handle(AlbumUpdatedEvent e, CancellationToken cancellationToken)
    {
        var album = await context.Albums.GetAsync(album => album.Id == e.Id, cancellationToken);

        if (e.Title is { Value: var title })
            album.Title = title;
        if (e.Description is { Value: var description })
            album.Description = description;
        if (e.Tags is { Value: var tags })
            album.Tags = tags;
        if (e.CategoryId is { Value: var categoryId })
            album.CategoryId = categoryId;
        if (e.Subscribes is { } subscribes)
            album.Subscribes = Array.ConvertAll(subscribes, s => s.Value);
    }
}
