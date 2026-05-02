using System.Diagnostics.CodeAnalysis;
using Domain.Entity;
using Domain.Shared.Converter;

namespace Domain.AlbumAggregate.ImageEntity;

[OpenJsonConverter<ImageTags, string[]>]
public sealed class ImageTags()
    : ValueObjects<ImageTags, string>,
        IFactoryConstructor<ImageTags, string[]>
{
    public const int MaxCount = 10;
    public const int MaxLength = 12;

    internal ImageTags(params string[] array)
        : this() => Value = array;

    public static bool TryCreateNew(string[] input, [NotNullWhen(true)] out ImageTags? entity)
    {
        entity = default;

        if (input is null || input.Length == 0)
        {
            entity = new();
            return true;
        }

        HashSet<string> set = [];
        for (int i = 0; i < input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(input[i]))
                continue;
            if (input[i].Length > MaxLength)
                return false;

            set.Add(input[i]);
        }

        if (set.Count > MaxCount)
            return false;

        entity = new([.. set]);
        return true;
    }
}
