using Domain.Album.Events;
using Domain.Event;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumCreatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumCreatedEvent>
{
    public async ValueTask Handle(AlbumCreatedEvent e, CancellationToken cancellationToken)
    {
        AlbumModel album = new(e);

        await context.Albums.AddAsync(album, cancellationToken);
    }
}
