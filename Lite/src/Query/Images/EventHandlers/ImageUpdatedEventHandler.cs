using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Images.EventHandlers;

public sealed class ImageUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<ImageUpdatedEvent>
{
    public async ValueTask Handle(ImageUpdatedEvent e, CancellationToken cancellationToken)
    {
        var image = await context.Images.GetAsync(
            image => image.Id == e.Id.Value,
            cancellationToken
        );

        if (e.Title is { Value: var title })
            image.Title = title;
        if (e.Tags is { Value: var tags })
            image.Tags = tags;
    }
}
