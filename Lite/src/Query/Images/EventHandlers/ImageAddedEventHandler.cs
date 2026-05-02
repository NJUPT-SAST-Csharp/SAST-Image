using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Images.EventHandlers;

public sealed class ImageAddedEventHandler(QueryDbContext context)
    : IDomainEventHandler<ImageAddedEvent>
{
    public async ValueTask Handle(ImageAddedEvent e, CancellationToken cancellationToken)
    {
        ImageModel image = new(e);

        await context.Images.AddAsync(image, cancellationToken);
    }
}
