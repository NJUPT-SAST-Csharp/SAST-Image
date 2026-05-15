namespace Client.Storage;

/// <summary>
/// In-memory storage backed by a dictionary. Useful for browser/WASM
/// scenarios where file-system access is unavailable.
/// </summary>
public sealed class MemoryStorage : IStorage
{
    private readonly Dictionary<string, string> _store = new();

    public Task<string?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        _store.TryGetValue(key, out var value);
        return Task.FromResult(value);
    }

    public Task SetAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        _store[key] = value;
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        _store.Remove(key);
        return Task.CompletedTask;
    }
}
