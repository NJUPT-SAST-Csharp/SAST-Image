using System.Diagnostics.CodeAnalysis;
using Domain.ValueObject;

namespace Domain.Album;

[OpenJsonConverter<AlbumTags, string[]>]
public sealed record class AlbumTags(params string[] Value) : IValueObject<AlbumTags, string[]>
{
    public const int MaxCount = 10;
    public const int MaxLength = 12;

    public static bool TryCreateNew(string[] input, [NotNullWhen(true)] out AlbumTags? entity)
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
