using Domain.ValueObject;
using Orleans.Concurrency;

namespace Storage.File;

[Alias("ImageFileManager")]
public interface IImageFileManagerGrain : IGrainWithGuidKey
{
    [Alias(nameof(GetAsync))]
    public Task<Immutable<byte[]?>> GetAsync<TId>(TId id, CancellationToken cancellationToken)
        where TId : ITypedId<TId>;
}
