namespace Client.Storage;

/// <summary>
/// Default file-based storage. Each key is persisted as a separate file
/// under the configured directory.
/// </summary>
public sealed class FileStorage : IStorage
{
    private readonly string _directory;

    /// <summary>
    /// Creates a FileStorage rooted at the compiled assembly's directory.
    /// </summary>
    public FileStorage()
        : this(DefaultDirectory) { }

    /// <param name="directory">Absolute path to the storage directory.</param>
    public FileStorage(string directory)
    {
        _directory = directory;
        Directory.CreateDirectory(_directory);
    }

    /// <summary>
    /// <c>AppContext.BaseDirectory</c> — the directory containing the compiled
    /// assembly (DLL/EXE). Falls back to <c>Environment.CurrentDirectory</c>.
    /// </summary>
    public static string DefaultDirectory { get; } = Path.Combine(
        AppContext.BaseDirectory,
        ".sastimg"
    );

    public Task<string?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        var path = GetPath(key);
        if (!File.Exists(path))
            return Task.FromResult<string?>(null);

        return Task.FromResult<string?>(File.ReadAllText(path));
    }

    public Task SetAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        var path = GetPath(key);
        var dir = Path.GetDirectoryName(path)!;
        Directory.CreateDirectory(dir);
        return File.WriteAllTextAsync(path, value, cancellationToken);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        var path = GetPath(key);
        if (File.Exists(path))
            File.Delete(path);
        return Task.CompletedTask;
    }

    private string GetPath(string key)
    {
        // Sanitize key to prevent directory traversal.
        var safeKey = key.Replace('/', '_').Replace('\\', '_').Replace("..", "_");
        return Path.Combine(_directory, $"{safeKey}.json");
    }
}
