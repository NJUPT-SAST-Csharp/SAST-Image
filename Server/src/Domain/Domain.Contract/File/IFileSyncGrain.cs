using Orleans.Concurrency;

namespace Domain.File;

[Alias("FileManagerGrain")]
public interface IFileSyncGrain : IGrainWithGuidKey
{
    [Alias(nameof(GetAsync))]
    public Task<Immutable<byte[]>> GetAsync(ImageFileKey key, CancellationToken cancellationToken);

    [Alias(nameof(UploadAsync))]
    public Task<ImageFileKey> UploadAsync(
        Immutable<byte[]> file,
        CancellationToken cancellationToken
    );
}
