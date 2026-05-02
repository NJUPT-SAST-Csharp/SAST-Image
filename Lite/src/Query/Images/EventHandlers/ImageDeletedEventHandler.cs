using Domain.AlbumAggregate.Events;
using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Images.EventHandlers;

public sealed class ImageDeletedEventHandler(QueryDbContext context)
    : IDomainEventHandler<ImageDeletedEvent>
{
    public async ValueTask Handle(ImageDeletedEvent e, CancellationToken cancellationToken)
    {
        await context
            .Images.Where(image => image.Id == e.Image.Value)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
