using Orleans.Concurrency;

namespace Domain.File;

[Alias("FileManagerGrain")]
public interface IFileManagerGrain : IGrainWithGuidKey
{
    [Alias(nameof(GetAsync))]
    public ValueTask<Immutable<byte[]>> GetAsync(
        ImageFileKey key,
        CancellationToken cancellationToken
    );

    [Alias(nameof(UploadAsync))]
    public ValueTask<ImageFileKey> UploadAsync(
        Immutable<byte[]> file,
        CancellationToken cancellationToken
    );
}
