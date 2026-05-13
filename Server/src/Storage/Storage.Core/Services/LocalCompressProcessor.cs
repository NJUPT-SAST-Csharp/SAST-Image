using Domain.ValueObject;
using Microsoft.Extensions.Options;
using NetVips;

namespace Storage.Services;

public sealed class LocalCompressProcessor(IOptions<StorageOptions> options)
{
    public async ValueTask CompressAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : ITypedId<TId>
    {
        await CompressAsync(id, null, cancellationToken);
    }

    public async ValueTask CompressAsync<TId>(
        TId id,
        string? sourceExtension,
        CancellationToken cancellationToken = default
    )
        where TId : ITypedId<TId>
    {
        await CompressAsync(id, sourceExtension, sourceExtension, cancellationToken);
    }

    public async ValueTask CompressAsync<TId>(
        TId id,
        string? sourceExtension,
        string? destinationExtension,
        CancellationToken cancellationToken = default
    )
        where TId : ITypedId<TId>
    {
        string original = id.AbsolutePath(options.Value.BaseUri.LocalPath, sourceExtension);
        string copy = Path.GetTempFileName();
        System.IO.File.Copy(original, copy, true);

        await using FileStream stream = new(
            copy,
            FileMode.OpenOrCreate,
            FileAccess.Read,
            FileShare.Read,
            4 * 1024,
            FileOptions.DeleteOnClose
        );

        using var image = NetVips.Image.NewFromStream(stream);
        string temp = Path.GetTempFileName();

        image.Heifsave(
            temp,
            q: options.Value.Quality,
            effort: options.Value.Effort,
            compression: Enums.ForeignHeifCompression.Av1,
            keep: Enums.ForeignKeep.All,
            lossless: false
        );

        string compressed = Path.ChangeExtension(original, destinationExtension);
        System.IO.File.Move(temp, compressed, true);
    }
}
