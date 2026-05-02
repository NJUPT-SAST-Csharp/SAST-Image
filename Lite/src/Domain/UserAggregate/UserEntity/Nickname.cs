using System.Diagnostics.CodeAnalysis;
using Domain.Entity;
using Domain.Shared.Converter;

namespace Domain.UserAggregate.UserEntity;

[OpenJsonConverter<Nickname, string>]
public readonly record struct Nickname(string Value)
    : IValueObject<Nickname, string>,
        IFactoryConstructor<Nickname, string>
{
    public const int MaxLength = 16;
    public const int MinLength = 1;

    public static bool TryCreateNew(string input, [NotNullWhen(true)] out Nickname newObject)
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
