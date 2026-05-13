using Domain.Event;
using Domain.File;
using Domain.User.Events;
using Mediator;
using Storage.Services;

namespace Storage.User.EventHandlers;

public sealed class AvatarUpdatedEventHandler(
    LocalImageFileManager manager,
    LocalCompressProcessor compressor,
    IGrainFactory factory
) : IDomainEventHandler<AvatarUpdatedEvent>
{
    public async ValueTask<Unit> Handle(AvatarUpdatedEvent e, CancellationToken cancellationToken)
    {
        var domainManager = factory.GetGrain<IFileSyncGrain>(Guid.Empty);
        var file = await domainManager.GetAsync(e.File, cancellationToken);
        await using MemoryStream stream = new(file.Value);

        await manager.SaveAsync(stream, e.Id, "avatar", cancellationToken);
        await compressor.CompressAsync(e.Id, "avatar", cancellationToken);

        return Unit.Value;
    }
}
