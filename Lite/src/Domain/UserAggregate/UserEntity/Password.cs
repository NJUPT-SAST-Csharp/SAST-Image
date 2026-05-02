using System.Diagnostics.CodeAnalysis;
using Domain.Entity;
using Domain.Shared;
using Domain.Shared.Converter;

namespace Domain.UserAggregate.UserEntity;

public readonly record struct Password
{
    [Obsolete("For ORM only", true)]
    public Password() { }

    public const int HashLength = 32;
    public const int SaltLength = 16;

    internal Password(byte[] hash, byte[] salt)
    {
        Hash = hash;
        Salt = salt;
    }

    public byte[] Hash { get; } = null!;
    public byte[] Salt { get; } = null!;
}

[OpenJsonConverter<PasswordInput, string>]
public readonly record struct PasswordInput(string Value)
    : IValueObject<PasswordInput, string>,
        IFactoryConstructor<PasswordInput, string>
{
    public const int MaxLength = 32;
    public const int MinLength = 6;

    public static bool TryCreateNew(string input, [NotNullWhen(true)] out PasswordInput newObject)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            newObject = default;
            return false;
        }

        input = input.Trim();

        if (input.Length < MinLength || input.Length > MaxLength)
        {
            newObject = default;
            return false;
        }

        if (input.IsLetterOrDigitOrUnderline() == false)
        {
            newObject = default;
            return false;
        }

        newObject = new(input);
        return true;
    }
}
