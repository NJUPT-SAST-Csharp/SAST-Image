using Domain.Album.Events;
using Domain.Event;
using Mediator;
using Query.Database;
using Query.Images;

namespace Query.Albums.EventHandlers;

public sealed class AlbumImageAddedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumImageAddedEvent>
{
    public async ValueTask<Unit> Handle(AlbumImageAddedEvent e, CancellationToken cancellationToken)
    {
        var album = await context.Albums.GetAsync(album => album.Id == e.Id, cancellationToken);

        album.Images.Add(new ImageModel(e));
        album.UpdatedAt = e.Timestamp;

        return Unit.Value;
    }
}
