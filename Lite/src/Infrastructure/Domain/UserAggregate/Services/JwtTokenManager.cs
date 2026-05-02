using System.Buffers.Binary;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.UserAggregate.UserEntity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Domain.UserAggregate.Services;

internal sealed class JwtTokenManager(IOptions<JwtAuthOptions> options) : IJwtTokenGenerator
{
    private static readonly JwtSecurityTokenHandler tokenHandler = new();
    private readonly JwtAuthOptions options = options.Value;
    private readonly SigningCredentials credentials = new(
        new SymmetricSecurityKey(Encoding.Default.GetBytes(options.Value.SecKey)),
        options.Value.Algorithm
    );

    public JwtToken Generate(UserId id, Username username, Roles roles)
    {
        var expireTime = DateTime.UtcNow.Add(TimeSpan.FromSeconds(options.Expires));

        List<Claim> claims =
        [
            new("id", id.Value.ToString()),
            new("username", username.Value),
            .. roles.Select(r => new Claim("role", r.ToString())),
        ];

        JwtSecurityToken tokenDescriptor = new(
            claims: claims,
            expires: expireTime,
            signingCredentials: credentials
        );

        string accessToken = tokenHandler.WriteToken(tokenDescriptor);

        string refreshToken = GenerateRefreshToken(id);

        return new(accessToken, new(refreshToken), options.Expires);
    }

    internal static string GenerateRefreshToken(UserId id)
    {
        var expiryTime = DateTime.UtcNow.AddDays(15);
        long expiryTimeBinary = expiryTime.ToBinary();

        Span<byte> tokenBytes = stackalloc byte[RefreshToken.ByteLength];

        BinaryPrimitives.WriteInt64LittleEndian(tokenBytes[0..8], id.Value);
        BinaryPrimitives.WriteInt64LittleEndian(tokenBytes[8..16], expiryTimeBinary);
        RandomNumberGenerator.Fill(tokenBytes[16..]);

        return Base64Url.EncodeToString(tokenBytes);
    }
}
