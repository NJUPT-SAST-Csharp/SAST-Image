using System.Globalization;
using Domain.ValueObject;
using IdGen;
using Microsoft.Extensions.Options;

namespace Storage.Services;

public sealed class LocalImageFileManager
{
    public const int BufferSize = 1024 * 256;
    private readonly string basePath;

    public LocalImageFileManager(IOptions<StorageOptions> options)
    {
        var uri = options.Value.BaseUri;
        if (uri is not { IsAbsoluteUri: true, IsFile: true, IsUnc: false })
        {
            throw new InvalidOperationException("The base URI must be a local file path.");
        }

        basePath = uri.LocalPath;
    }

    public async ValueTask SaveAsync<TId>(
        Stream stream,
        TId id,
        CancellationToken cancellationToken = default
    )
        where TId : ITypedId<TId, long>
    {
        await SaveAsync(stream, id, null, cancellationToken);
    }

    public async ValueTask SaveAsync<TId>(
        Stream stream,
        TId id,
        string? extension,
        CancellationToken cancellationToken = default
    )
        where TId : ITypedId<TId, long>
    {
        string destination = id.AbsolutePath(basePath, extension);

        EnsureDirectory(destination);
        var filename = Path.GetTempFileName();

        await using (var file = System.IO.File.OpenWrite(filename))
        {
            await stream.CopyToAsync(file, BufferSize, cancellationToken);
        }
        System.IO.File.Move(filename, destination, true);
    }

    public Stream? GetStream<TId>(TId id)
        where TId : ITypedId<TId, long> => GetStream(id, null);

    public Stream? GetStream<TId>(TId id, string? extension)
        where TId : ITypedId<TId, long>
    {
        string filename = id.AbsolutePath(basePath, extension);
        return System.IO.File.Exists(filename) ? System.IO.File.OpenRead(filename) : null;
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

internal static class IdExtensions
{
    extension<TId>(TId id)
        where TId : ITypedId<TId, long>
    {
        public string RelativePath
        {
            get
            {
                var epoch = IdGeneratorOptions.DefaultEpoch;
                long timestamp = (id.Value >> 22);

                if (timestamp <= 0)
                {
                    return id.Value.ToString(CultureInfo.InvariantCulture);
                }

                var dateTime = epoch.AddMilliseconds(timestamp);

                return dateTime.ToString($"yyyy/MM/dd/{id.Value}", CultureInfo.InvariantCulture);
            }
        }

        public string AbsolutePath(string root)
        {
            return Path.Combine(root, id.RelativePath);
        }

        public string AbsolutePath(string root, string? extension)
        {
            return Path.ChangeExtension(Path.Combine(root, id.RelativePath), extension);
        }
    }
}

internal static class StreamExtensions
{
    extension(Stream file)
    {
        public string? Extension
        {
            get
            {
                var postion = file.Position;
                string loader = NetVips.Image.FindLoadStream(file);
                file.Seek(postion, SeekOrigin.Begin);

                if (string.IsNullOrWhiteSpace(loader))
                    return null;

                var span = loader.AsSpan();
                Span<char> buffer = stackalloc char[span.Length];
                span.ToLowerInvariant(buffer);

                const string prefix = "vipsforeignload";
                const string suffix = "file";

                var trimmed = buffer;
                if (trimmed.StartsWith(prefix, StringComparison.Ordinal))
                    trimmed = trimmed[prefix.Length..];

                if (trimmed.EndsWith(suffix, StringComparison.Ordinal))
                    trimmed = trimmed[..^suffix.Length];

                return new string(trimmed);
            }
        }
    }
}
