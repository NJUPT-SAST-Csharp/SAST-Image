using App.Framework.Interop;
using Client.Storage;

namespace App.Components;

/// <summary>IStorage implementation backed by browser localStorage.</summary>
public sealed class BrowserStorage : IStorage
{
    public Task<string?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        var result = DomInterop.LocalStorageGet(key);
        return Task.FromResult(result);
    }

    public Task SetAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        DomInterop.LocalStorageSet(key, value);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        DomInterop.LocalStorageRemove(key);
        return Task.CompletedTask;
    }
}
