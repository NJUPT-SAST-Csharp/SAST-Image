using System.Diagnostics.CodeAnalysis;
using Domain.ValueObject;

namespace Domain.Category;

[OpenJsonConverter<CategoryDescription, string>]
public readonly record struct CategoryDescription(string Value)
    : IValueObject<CategoryDescription, string>
{
    public const int MaxLength = 100;

    public static bool TryCreateNew(
        string input,
        [NotNullWhen(true)] out CategoryDescription newObject
    )
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            newObject = default;
            return false;
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
