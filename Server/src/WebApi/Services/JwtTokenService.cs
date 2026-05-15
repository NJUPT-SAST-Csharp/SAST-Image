using System.Buffers;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain;
using Domain.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Services;

public sealed class JwtTokenService(IOptions<AuthOptions> options)
{
    private readonly AuthOptions _options = options.Value;
    private readonly SigningCredentials _credentials = new(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecKey)),
        options.Value.Algorithm
    );

    public JwtToken Generate(UserId userId, Username username, Role role)
    {
        var expireTime = DateTime.UtcNow.Add(TimeSpan.FromSeconds(_options.Expires));

        var claims = new List<Claim>
        {
            new("id", userId.Value.ToString()),
            new("username", username.Value),
            new("role", ((byte)role).ToString()),
        };

        var tokenDescriptor = new JwtSecurityToken(
            claims: claims,
            expires: expireTime,
            signingCredentials: _credentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        var refreshToken = GenerateRefreshToken(userId);

        return new JwtToken(accessToken, refreshToken, _options.Expires);
    }

    public (UserId userId, bool isValid) DecodeRefreshToken(string token)
    {
        Span<byte> bytes = stackalloc byte[32];
        if (Base64Url.DecodeFromChars(token, bytes, out _, out int bytesWritten) != OperationStatus.Done || bytesWritten < 16)
            return (new(0), false);

        var userId = BinaryPrimitives.ReadInt64LittleEndian(bytes[0..8]);
        var expiryBinary = BinaryPrimitives.ReadInt64LittleEndian(bytes[8..16]);
        var expiryTime = DateTime.FromBinary(expiryBinary);

        return (new(userId), expiryTime > DateTime.UtcNow);
    }

    private static string GenerateRefreshToken(UserId userId)
    {
        var expiryTime = DateTime.UtcNow.AddDays(15);
        Span<byte> tokenBytes = stackalloc byte[32];
        BinaryPrimitives.WriteInt64LittleEndian(tokenBytes[0..8], userId.Value);
        BinaryPrimitives.WriteInt64LittleEndian(tokenBytes[8..16], expiryTime.ToBinary());
        RandomNumberGenerator.Fill(tokenBytes[16..]);
        return Base64Url.EncodeToString(tokenBytes);
    }
}

public sealed record JwtToken(string AccessToken, string RefreshToken, long ExpireIn);

public sealed class AuthOptions
{
    public required string SecKey { get; init; }
    public required string Algorithm { get; init; }
    public required int Expires { get; init; }
}
