using Domain.Album.Events;
using Domain.Event;
using Mediator;
using Query.Database;

namespace Query.Album.EventHandlers;

public sealed class AlbumImageUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumImageUpdatedEvent>
{
    public async ValueTask<Unit> Handle(
        AlbumImageUpdatedEvent e,
        CancellationToken cancellationToken
    )
    {
        var image = await context.Images.GetAsync(image => image.Id == e.Id, cancellationToken);

        if (e.Title is { Value: var title })
            image.Title = title;
        if (e.Tags is { Value: var tags })
            image.Tags = tags;
        if (e.Likes is { } likes)
            image.Likes = Array.ConvertAll(likes, l => l.Value);

        return Unit.Value;
    }
}
