using Domain.AlbumAggregate.Events;
using Domain.Event;
using Storage.Albums.Messages;
using Storage.Database;

namespace Storage.Albums.EventHandlers;

public sealed class AlbumCoverUpdatedEventHandler(
    IImageFileManager manager,
    StorageDbContext context
) : IDomainEventHandler<AlbumCoverUpdatedEvent>
{
    public async ValueTask Handle(AlbumCoverUpdatedEvent e, CancellationToken cancellationToken)
    {
        var message = e switch
        {
            AlbumCoverUpdatedManuallyEvent { Album: var album, File: var file } =>
                new AlbumCoverUpdatedMessage(DateTime.UtcNow, album, file),
            AlbumCoverUpdatedAutomaticallyEvent { Album: var album, Image: not { } } =>
                new AlbumCoverUpdatedMessage(DateTime.UtcNow, album, null),
            AlbumCoverUpdatedAutomaticallyEvent { Album: var album, Image: { } image }
                when manager.TryGet(image, out var file) => new AlbumCoverUpdatedMessage(
                DateTime.UtcNow,
                album,
                file
            ),

            _ => throw new InvalidOperationException(
                $"Unsupported event type: {e.GetType().FullName}"
            ),
        };

        await context.Messages.AddAsync(message, cancellationToken);
    }
}
