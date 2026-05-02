using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Images.EventHandlers;

public sealed class ImageRemovedEventHandler(QueryDbContext context)
    : IDomainEventHandler<ImageRemovedEvent>
{
    public async ValueTask Handle(ImageRemovedEvent e, CancellationToken cancellationToken)
    {
        var image = await context.Images.GetAsync(
            image => image.Id == e.Image.Value,
            cancellationToken
        );

        image.Remove(e);
    }
}
