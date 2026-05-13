using Domain.Album.Events;
using Domain.Event;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Album.EventHandlers;

public sealed class AlbumImageRemovedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumImageRemovedEvent>
{
    public async ValueTask<Unit> Handle(
        AlbumImageRemovedEvent e,
        CancellationToken cancellationToken
    )
    {
        await context
            .Images.Where(i => i.Id == e.ImageId.Value)
            .ExecuteDeleteAsync(cancellationToken);

        return Unit.Value;
    }
}
