using System.Diagnostics.CodeAnalysis;
using Domain.Entity;
using Domain.Shared.Converter;

namespace Domain.AlbumAggregate.AlbumEntity;

[OpenJsonConverter<AlbumTags, string[]>]
public sealed class AlbumTags()
    : ValueObjects<AlbumTags, string>,
        IFactoryConstructor<AlbumTags, string[]>
{
    public const int MaxCount = 10;
    public const int MaxLength = 12;

    internal AlbumTags(params string[] array)
        : this() => Value = array;

    public static bool TryCreateNew(string[] input, [NotNullWhen(true)] out AlbumTags? entity)
    {
        entity = default;

        if (input.Length == 0)
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
