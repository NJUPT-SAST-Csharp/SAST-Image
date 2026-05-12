using System.Diagnostics.CodeAnalysis;
using Domain.ValueObject;

namespace Domain.Album.Image;

public readonly record struct ImageTitle(string Value) : IValueObject<ImageTitle, string>
{
    public const int MaxLength = 20;

    public static readonly ImageTitle Empty = new(string.Empty);

    public static bool TryCreateNew(string value, [NotNullWhen(true)] out ImageTitle newObject)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            newObject = Empty;
            return true;
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            newObject = default;
            return false;
        }

        newObject = new(value);
        return true;
    }
}
