using Domain.Album.Events;
using Domain.Event;
using Mediator;
using Storage.Services;

namespace Storage.Images.EventHandlers;

public sealed class AlbumImageRemovedEventHandler(IImageFileManager manager)
    : IDomainEventHandler<AlbumImageRemovedEvent>
{
    public async ValueTask<Unit> Handle(
        AlbumImageRemovedEvent e,
        CancellationToken cancellationToken
    )
    {
        await manager.DeleteAsync(e.ImageId, cancellationToken);

        return Unit.Value;
    }
}
