using System.Diagnostics.CodeAnalysis;
using Domain.Entity;

namespace Domain.CategoryAggregate.CategoryEntity;

public readonly record struct CategoryName(string Value)
    : IValueObject<CategoryName, string>,
        IFactoryConstructor<CategoryName, string>
{
    public const int MaxLength = 10;
    public const int MinLength = 2;

    public static bool TryCreateNew(string input, [NotNullWhen(true)] out CategoryName newObject)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            newObject = default;
            return false;
        }

        input = input.Trim();

        if (input.Length > MaxLength || input.Length < MinLength)
        {
            newObject = default;
            return false;
        }

        newObject = new(input);
        return true;
    }
}
