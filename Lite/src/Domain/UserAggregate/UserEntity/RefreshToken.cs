using System.Buffers.Binary;
using System.Buffers.Text;
using System.Diagnostics.CodeAnalysis;
using Domain.Entity;
using Domain.Shared.Converter;

namespace Domain.UserAggregate.UserEntity;

[OpenJsonConverter<RefreshToken, string>]
public readonly record struct RefreshToken(string Value)
    : IValueObject<RefreshToken, string>,
        IFactoryConstructor<RefreshToken, string>
{
    public const int ByteLength = 32;

    internal UserId Id
    {
        get
        {
            Span<byte> buffer = stackalloc byte[ByteLength];
            Base64Url.TryDecodeFromChars(Value, buffer, out _);

            long id = BinaryPrimitives.ReadInt64LittleEndian(buffer[0..8]);

            return new(id);
        }
    }

    public static bool TryCreateNew(string input, [NotNullWhen(true)] out RefreshToken newObject)
    {
        Span<byte> buffer = stackalloc byte[ByteLength];

        if (
            Base64Url.IsValid(input) == false
            || Base64Url.TryDecodeFromChars(input, buffer, out int count) == false
            || count != ByteLength
        )
        {
            newObject = default;
            return false;
        }

        if (
            BinaryPrimitives.TryReadInt64LittleEndian(buffer[0..8], out long userId) == false
            || userId < 0
            || BinaryPrimitives.TryReadInt64LittleEndian(buffer[8..16], out long timeB) == false
            || DateTime.FromBinary(timeB) < DateTime.UtcNow
        )
        {
            newObject = default;
            return false;
        }

        newObject = new(input);
        return true;
    }

    public override string ToString() => Value;
}
