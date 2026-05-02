using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Domain.Entity;
using Domain.Shared;
using IdGen;

namespace Storage;

public static class DomainModelExtensions
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

    extension(ImageFile file)
    {
        /// <summary>
        /// Without dot (.) and in lower case, e.g., "jpg", "png", "webp", etc.
        /// </summary>
        public bool TryGetExtension([NotNullWhen(true)] out string? extension)
        {
            extension = file.Extension;
            return extension != null;
        }

        public string? Extension
        {
            get
            {
                if (file.TryGetValue(out string? value) is false)
                    return null;
                string loader = NetVips.Image.FindLoad(value);

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
