using Domain.AlbumAggregate.Events;
using Domain.Event;
using Storage.Database;
using Storage.Images.Messages;

namespace Storage.Images.EventHandlers;

public sealed class ImageAddedEventHandler(StorageDbContext context)
    : IDomainEventHandler<ImageAddedEvent>
{
    public async ValueTask Handle(ImageAddedEvent e, CancellationToken cancellationToken)
    {
        await context.Messages.AddAsync(
            new ImageAddedMessage(e.CreatedAt, e.File, e.ImageId),
            cancellationToken
        );
    }
}
