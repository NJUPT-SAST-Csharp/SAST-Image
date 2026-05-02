using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using Domain.Entity;
using Domain.Shared.Converter;

namespace Domain.UserAggregate.UserEntity;

[OpenJsonConverter<RegistryCode, int>]
public readonly record struct RegistryCode(int Value)
    : IValueObject<RegistryCode, int>,
        IFactoryConstructor<RegistryCode, int>
{
    public const int MinValue = 100000;
    public const int MaxValue = 999999;

    public static RegistryCode GenerateNew() =>
        new(RandomNumberGenerator.GetInt32(MinValue, MaxValue));

    public static bool TryCreateNew(int input, [NotNullWhen(true)] out RegistryCode newObject)
    {
        if (input < 100000 || input > 999999)
        {
            newObject = default;
            return false;
        }

        newObject = new(input);
        return true;
    }
}
