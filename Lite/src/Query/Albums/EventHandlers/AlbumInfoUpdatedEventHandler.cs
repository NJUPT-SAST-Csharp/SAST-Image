using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumInfoUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumInfoUpdatedEvent>
{
    public async ValueTask Handle(AlbumInfoUpdatedEvent e, CancellationToken cancellationToken)
    {
        var album = await context.Albums.GetAsync(
            album => album.Id == e.Id.Value,
            cancellationToken
        );

        if (e.Title is { Value: var title })
            album.Title = title;
        if (e.Description is { Value: var description })
            album.Description = description;
        if (e.Tags is { Value: var tags })
            album.Tags = tags;
    }
}
