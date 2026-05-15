using Client.Storage;

namespace Client;

public sealed record ClientOptions
{
    public string BaseUrl { get; init; } = "http://localhost:5078";
    public IStorage Storage { get; init; } = new MemoryStorage();
}
