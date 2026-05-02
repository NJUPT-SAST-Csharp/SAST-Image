using System.Buffers.Binary;
using System.Buffers.Text;
using System.Security.Cryptography;
using Domain.UserAggregate.UserEntity;
using Shouldly;

namespace Domain.Tests.UserEntity;

[TestClass]
public sealed class RefreshTokenTests
{
    private static string BuildToken(int byteLength, long userId, DateTime expiresAtUtc)
    {
        Span<byte> buffer = stackalloc byte[byteLength];

        BinaryPrimitives.WriteInt64LittleEndian(buffer[0..8], userId);
        BinaryPrimitives.WriteInt64LittleEndian(buffer[8..16], expiresAtUtc.ToBinary());

        if (byteLength > 16)
        {
            RandomNumberGenerator.Fill(buffer[16..]);
        }

        return Base64Url.EncodeToString(buffer);
    }

    private static IEnumerable<object?[]> Valid_Values
    {
        get
        {
            yield return [BuildToken(RefreshToken.ByteLength, 2333, DateTime.UtcNow.AddDays(1))];
            yield return [BuildToken(RefreshToken.ByteLength, 0, DateTime.UtcNow.AddDays(1))];
        }
    }

    [TestMethod]
    [DynamicData(nameof(Valid_Values))]
    public void Return_True_When_Create_From_Valid(string value)
    {
        bool result = RefreshToken.TryCreateNew(value!, out var token);

        result.ShouldBeTrue();
        token.Value.ShouldBe(value);
    }

    private static IEnumerable<object?[]> Invalid_Values
    {
        get
        {
            yield return
            [
                BuildToken(RefreshToken.ByteLength + 1, 2333, DateTime.UtcNow.AddDays(1)),
            ];
            yield return [BuildToken(RefreshToken.ByteLength, -2333, DateTime.UtcNow.AddDays(1))];
            yield return
            [
                BuildToken(RefreshToken.ByteLength, 2333, DateTime.UtcNow.AddSeconds(-1)),
            ];
            yield return
            [
                BuildToken(RefreshToken.ByteLength - 1, 2333, DateTime.UtcNow.AddDays(1)),
            ];

            yield return ["invalid_base64"];
            yield return [string.Empty];
            yield return ["   "];
            yield return [null];
        }
    }

    [TestMethod]
    [DynamicData(nameof(Invalid_Values))]
    public void Return_False_When_Create_From_Invalid(string? value)
    {
        bool result = RefreshToken.TryCreateNew(value!, out var token);

        result.ShouldBeFalse();
        token.ShouldBe(default);
    }
}
