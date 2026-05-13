using Domain.Event;
using Domain.File;
using Domain.User.Events;
using Mediator;
using Storage.Services;

namespace Storage.User.EventHandlers;

public sealed class HeaderUpdatedEventHandler(
    LocalImageFileManager manager,
    LocalCompressProcessor compressor,
    IGrainFactory factory
) : IDomainEventHandler<HeaderUpdatedEvent>
{
    public async ValueTask<Unit> Handle(HeaderUpdatedEvent e, CancellationToken cancellationToken)
    {
        var domainManager = factory.GetGrain<IFileSyncGrain>(Guid.Empty);
        var file = await domainManager.GetAsync(e.File, cancellationToken);

        await using MemoryStream ms = new(file.Value);

        await manager.SaveAsync(ms, e.Id, "header", cancellationToken);
        await compressor.CompressAsync(e.Id, "header", cancellationToken);

        return Unit.Value;
    }
}
