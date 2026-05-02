using System.Diagnostics.CodeAnalysis;
using Domain.Entity;
using Domain.Shared;
using Microsoft.Extensions.Options;

namespace Storage;

public interface IImageFileManager
{
    public ValueTask SaveAsync<TId>(
        ImageFile file,
        TId id,
        CancellationToken cancellationToken = default
    )
        where TId : ITypedId<TId, long>;

    public ValueTask SaveAsync<TId>(
        ImageFile file,
        TId id,
        string? extension,
        CancellationToken cancellationToken
    )
        where TId : ITypedId<TId, long>;

    public bool TryGet<TId>(TId id, [NotNullWhen(true)] out ImageFile? file)
        where TId : ITypedId<TId, long>;
    public bool TryGet<TId>(TId id, string? extension, [NotNullWhen(true)] out ImageFile? file)
        where TId : ITypedId<TId, long>;

    public ValueTask DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : ITypedId<TId, long>;
}

internal sealed class LocalImageFileManager : IImageFileManager
{
    private readonly string basePath;

    public LocalImageFileManager(IOptions<StorageOptions> options)
    {
        basePath = options.Value.BaseUri.LocalPath;
        if (
            options.Value.BaseUri.IsAbsoluteUri is false
            || options.Value.BaseUri.IsFile is false
            || options.Value.BaseUri.IsUnc
        )
        {
            throw new InvalidOperationException("The base URI must be a local file path.");
        }
    }

    public async ValueTask SaveAsync<TId>(
        ImageFile file,
        TId id,
        CancellationToken cancellationToken = default
    )
        where TId : ITypedId<TId, long>
    {
        await SaveAsync(file, id, null, cancellationToken);
    }

    public async ValueTask SaveAsync<TId>(
        ImageFile file,
        TId id,
        string? extension,
        CancellationToken cancellationToken = default
    )
        where TId : ITypedId<TId, long>
    {
        if (file.TryGetValue(out string? value) is false)
        {
            throw new InvalidOperationException("The source must be a local file path.");
        }

        string destination = id.AbsolutePath(basePath, extension);

        EnsureDirectory(destination);
        File.Move(value, destination, overwrite: true);
    }

    public bool TryGet<TId>(TId id, [NotNullWhen(true)] out ImageFile? file)
        where TId : ITypedId<TId, long>
    {
        return TryGet(id, null, out file);
    }

    public bool TryGet<TId>(TId id, string? extension, [NotNullWhen(true)] out ImageFile? file)
        where TId : ITypedId<TId, long>
    {
        string filename = id.AbsolutePath(basePath, extension);
        bool result = ImageFile.TryCreateNew(filename, out var image);
        file = result ? image : null;
        return result;
    }

    public async ValueTask DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : ITypedId<TId, long>
    {
        string filename = id.AbsolutePath(basePath);
        string? directory = Path.GetDirectoryName(filename);
        if (Directory.Exists(directory))
        {
            Directory.Delete(directory, true);
        }
    }

    internal static void EnsureDirectory(string filename)
    {
        string path = Path.GetDirectoryName(filename)!;
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}
