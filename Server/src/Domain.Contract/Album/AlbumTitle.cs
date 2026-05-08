using System.Diagnostics.CodeAnalysis;
using Domain.ValueObject;

namespace Domain.Album;

[OpenJsonConverter<AlbumTitle, string>]
public readonly record struct AlbumTitle(string Value) : IValueObject<AlbumTitle, string>
{
    public const int MaxLength = 20;
    public const int MinLength = 1;

    public static bool TryCreateNew(string value, [NotNullWhen(true)] out AlbumTitle newObject)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            newObject = default;
            return false;
        }

        value = value.Trim();

        if (value.Length > MaxLength || value.Length < MinLength)
        {
            newObject = default;
            return false;
        }

        newObject = new(value);
        return true;
    }
}
