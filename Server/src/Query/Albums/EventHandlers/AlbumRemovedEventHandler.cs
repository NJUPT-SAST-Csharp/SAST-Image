using Domain.Album.Events;
using Domain.Event;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumRemovedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumRemovedEvent>
{
    public async ValueTask<Unit> Handle(AlbumRemovedEvent e, CancellationToken cancellationToken)
    {
        await context.Albums.Where(a => a.Id == e.Id).ExecuteDeleteAsync(cancellationToken);

        return Unit.Value;
    }
}
