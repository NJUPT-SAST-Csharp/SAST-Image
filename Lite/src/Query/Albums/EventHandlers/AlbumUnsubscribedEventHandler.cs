using Domain.AlbumAggregate.Events;
using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Albums.EventHandlers;

public sealed class AlbumUnsubscribedEventHandler(QueryDbContext context)
    : IDomainEventHandler<AlbumUnsubscribedEvent>
{
    public async ValueTask Handle(AlbumUnsubscribedEvent e, CancellationToken cancellationToken)
    {
        await context
            .Subscribes.Where(s => s.Album == e.Album.Value)
            .Where(s => s.User == e.User.Value)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
