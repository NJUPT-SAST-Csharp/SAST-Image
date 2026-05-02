using System.Text;
using Domain.UserAggregate.UserEntity;
using Konscious.Security.Cryptography;

namespace Domain.UserAggregate.Services;

internal sealed class PasswordGenerator : IPasswordGenerator
{
    public async Task<Password> GenerateAsync(
        PasswordInput password,
        CancellationToken cancellationToken
    )
    {
        byte[] salt = new byte[Password.SaltLength];
        Random.Shared.NextBytes(salt);

        using Argon2 argon = new Argon2id(Encoding.Default.GetBytes(password.Value));
        argon.Iterations = 8;
        argon.MemorySize = 4096;
        argon.DegreeOfParallelism = 1;
        argon.Salt = salt;
        byte[] hash = await argon.GetBytesAsync(Password.HashLength);

        return new(hash, salt);
    }
}
