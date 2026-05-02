using System.Diagnostics.CodeAnalysis;
using Domain.Entity;
using Domain.Shared.Converter;

namespace Domain.UserAggregate.UserEntity;

[OpenJsonConverter<Username, string>]
public readonly record struct Username(string Value)
    : IValueObject<Username, string>,
        IFactoryConstructor<Username, string>
{
    private static readonly System.Buffers.SearchValues<char> AllowedChars =
        System.Buffers.SearchValues.Create(
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_"
        );

    public const int MaxLength = 16;
    public const int MinLength = 2;

    public static bool TryCreateNew(string input, [NotNullWhen(true)] out Username newObject)
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

        if (input.AsSpan().ContainsAnyExcept(AllowedChars))
        {
            newObject = default;
            return false;
        }

        newObject = new(input);
        return true;
    }
}
