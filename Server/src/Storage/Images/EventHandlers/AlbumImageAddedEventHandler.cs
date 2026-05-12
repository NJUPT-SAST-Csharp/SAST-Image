using Domain.Album.Events;
using Domain.Event;
using Domain.File;
using Mediator;
using Storage.Services;

namespace Storage.Images.EventHandlers;

public sealed class AlbumImageAddedEventHandler(
    IImageFileManager manager,
    ICompressProcessor compressor,
    IGrainFactory factory
) : IDomainEventHandler<AlbumImageAddedEvent>
{
    public async ValueTask<Unit> Handle(AlbumImageAddedEvent e, CancellationToken cancellationToken)
    {
        var domainManager = factory.GetGrain<IFileManagerGrain>(Guid.Empty);
        var file = await domainManager.GetAsync(e.File, cancellationToken);

        await using MemoryStream ms = new(file.Value);

        await manager.SaveAsync(ms, e.Id, cancellationToken);

        await compressor.CompressAsync(e.Id, null, "compressed", cancellationToken);

        return Unit.Value;
    }
}
