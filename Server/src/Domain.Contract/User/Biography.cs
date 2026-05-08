using System.Diagnostics.CodeAnalysis;
using Domain.ValueObject;

namespace Domain.User;

[OpenJsonConverter<Biography, string>]
public readonly record struct Biography(string Value) : IValueObject<Biography, string>
{
    public static readonly Biography Empty = new(string.Empty);

    public const int MaxLength = 50;

    public static bool TryCreateNew(string input, [NotNullWhen(true)] out Biography newObject)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            newObject = Empty;
            return true;
        }

        input = input.Trim();

        if (input.Length > MaxLength)
        {
            newObject = default;
            return false;
        }

        newObject = new(input);
        return true;
    }
}
