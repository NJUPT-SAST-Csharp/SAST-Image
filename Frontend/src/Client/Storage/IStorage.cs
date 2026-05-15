namespace Client.Storage;

/// <summary>
/// Abstraction for persisting data (JWT tokens, preferences, etc.).
/// Implement this interface to plug in your own storage backend
/// (in-memory, database, encrypted file, etc.).
/// </summary>
public interface IStorage
{
    Task<string?> GetAsync(string key, CancellationToken cancellationToken = default);
    Task SetAsync(string key, string value, CancellationToken cancellationToken = default);
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}
