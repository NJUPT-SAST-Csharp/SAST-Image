using Domain.Album.Events;
using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumImageRemovedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumImageRemovedEvent>
{
    public async ValueTask Handle(AlbumImageRemovedEvent e, CancellationToken cancellationToken)
    {
        await context
            .Images.Where(i => i.Id == e.ImageId.Value)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
