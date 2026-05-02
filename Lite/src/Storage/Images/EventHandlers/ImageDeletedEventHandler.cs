using Domain.AlbumAggregate.Events;
using Domain.Event;
using Storage.Database;
using Storage.Images.Messages;

namespace Storage.Images.EventHandlers;

public sealed class ImageDeletedEventHandler(StorageDbContext context)
    : IDomainEventHandler<ImageDeletedEvent>
{
    public async ValueTask Handle(ImageDeletedEvent e, CancellationToken cancellationToken)
    {
        await context.Messages.AddAsync(
            new ImageDeletedMessage(DateTime.UtcNow, e.Image),
            cancellationToken
        );
    }
}
