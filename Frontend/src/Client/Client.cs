using Client.Storage;
using Client.SignalR;

namespace Client;

public sealed class SastClient
{
    private SignalRClient? _signalR;

    public ClientOptions Options { get; }

    public SastClient(string baseUrl, IStorage storage)
        => Options = new ClientOptions { BaseUrl = baseUrl, Storage = storage };

    public SastClient(ClientOptions options)
        => Options = options;

    public SignalRClient SignalR() => _signalR ??= new(Options.BaseUrl, Options.Storage);
}
