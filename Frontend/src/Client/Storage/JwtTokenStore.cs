using System.Text.Json;

namespace Client.Storage;

public sealed record JwtToken(string AccessToken, string RefreshToken, long ExpireIn);

public sealed class JwtTokenStore
{
    private const string TokenKey = "sastimg.jwt";
    private readonly IStorage _storage;

    public JwtTokenStore(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<JwtToken?> LoadAsync(CancellationToken cancellationToken = default)
    {
        var json = await _storage.GetAsync(TokenKey, cancellationToken);
        if (json is null) return null;
        try { return JsonSerializer.Deserialize<JwtToken>(json); }
        catch { return null; }
    }

    public Task SaveAsync(JwtToken token, CancellationToken cancellationToken = default)
        => _storage.SetAsync(TokenKey, JsonSerializer.Serialize(token), cancellationToken);

    public Task ClearAsync(CancellationToken cancellationToken = default)
        => _storage.RemoveAsync(TokenKey, cancellationToken);
}
