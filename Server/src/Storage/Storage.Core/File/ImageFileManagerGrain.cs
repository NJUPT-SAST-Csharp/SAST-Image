using Domain.ValueObject;
using Orleans.Concurrency;
using Storage.Services;

namespace Storage.File;

[StatelessWorker]
internal sealed class ImageFileManagerGrain(LocalImageFileManager manager)
    : Grain,
        IImageFileManagerGrain
{
    public async Task<Immutable<byte[]?>> GetAsync<TId>(TId id, CancellationToken cancellationToken)
        where TId : ITypedId<TId>
    {
        await using var stream = manager.GetStream(id);

        if (stream is null)
            return new Immutable<byte[]?>(null);

        await using var memoryStream = new MemoryStream();

        await stream.CopyToAsync(memoryStream, LocalImageFileManager.BufferSize, cancellationToken);

        return new Immutable<byte[]?>(memoryStream.ToArray());
    }
}
