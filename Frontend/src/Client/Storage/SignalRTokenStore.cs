using System.Text.Json;

namespace Client.Storage;

public sealed class SignalRTokenStore(IStorage storage)
{
    private const string TokenKey = "sastimg.jwt";

    public async Task<SignalRToken?> LoadAsync(CancellationToken cancellationToken = default)
    {
        var json = await storage.GetAsync(TokenKey, cancellationToken);
        if (json is null) return null;
        try { return JsonSerializer.Deserialize<SignalRToken>(json); }
        catch { return null; }
    }

    public Task SaveAsync(SignalRToken token, CancellationToken cancellationToken = default)
        => storage.SetAsync(TokenKey, JsonSerializer.Serialize(token), cancellationToken);

    public Task ClearAsync(CancellationToken cancellationToken = default)
        => storage.RemoveAsync(TokenKey, cancellationToken);
}

public sealed record SignalRToken(string AccessToken, string RefreshToken, long ExpireIn);
