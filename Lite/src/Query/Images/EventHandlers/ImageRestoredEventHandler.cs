using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Images.EventHandlers;

public sealed class ImageRestoredEventHandler(QueryDbContext context)
    : IDomainEventHandler<ImageRestoredEvent>
{
    public async ValueTask Handle(ImageRestoredEvent e, CancellationToken cancellationToken)
    {
        var image = await context.Images.GetAsync(
            image => image.Id == e.Image.Value,
            cancellationToken
        );

        image.Restore(e);
    }
}
