using Client.Storage;
using Domain.Api;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.SignalR;

public class SignalRClient : IAsyncDisposable
{
    private readonly HubConnection _connection;
    private readonly SignalRTokenStore _tokenStore;
    private bool _ownsConnection;

    public HubConnection Connection => _connection;

    public SignalRClient(string baseUrl, IStorage storage)
    {
        _tokenStore = new SignalRTokenStore(storage);

        _connection = new HubConnectionBuilder()
            .WithUrl($"{baseUrl.TrimEnd('/')}/hub", options =>
            {
                options.AccessTokenProvider = async () =>
                {
                    var token = await _tokenStore.LoadAsync();
                    return token?.AccessToken;
                };
            })
            .WithAutomaticReconnect()
            .Build();

        _ownsConnection = true;
    }

    public SignalRClient(HubConnection connection, IStorage storage)
    {
        _tokenStore = new SignalRTokenStore(storage);
        _connection = connection;
        _ownsConnection = false;
    }

    private async Task EnsureConnected()
    {
        if (_connection.State != HubConnectionState.Connected)
            await _connection.StartAsync();
    }

    private async Task RestartConnectionAsync()
    {
        if (_connection.State == HubConnectionState.Connected)
            await _connection.StopAsync();
        await _connection.StartAsync();
    }

    // ─── Account ────────────────────────────────────────────────

    public async Task<JwtTokenResponse> LoginAsync(string username, string password)
    {
        await EnsureConnected();
        var result = await _connection.InvokeAsync<JwtTokenResponse>("Login", new LoginRequest(username, password));
        await _tokenStore.SaveAsync(new SignalRToken(result.AccessToken, result.RefreshToken, result.ExpireIn));
        await RestartConnectionAsync();
        return result;
    }

    public async Task<JwtTokenResponse> RegisterAsync(RegisterRequest request)
    {
        await EnsureConnected();
        var result = await _connection.InvokeAsync<JwtTokenResponse>("Register", request);
        await _tokenStore.SaveAsync(new SignalRToken(result.AccessToken, result.RefreshToken, result.ExpireIn));
        await RestartConnectionAsync();
        return result;
    }

    public async Task<JwtTokenResponse> RefreshTokenAsync(string refreshToken)
    {
        await EnsureConnected();
        var result = await _connection.InvokeAsync<JwtTokenResponse>("RefreshToken", refreshToken);
        await _tokenStore.SaveAsync(new SignalRToken(result.AccessToken, result.RefreshToken, result.ExpireIn));
        await RestartConnectionAsync();
        return result;
    }

    public async Task<string?> GetStoredAccessTokenAsync()
    {
        var token = await _tokenStore.LoadAsync();
        return token?.AccessToken;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _tokenStore.LoadAsync();
        return token is not null;
    }

    public async Task LogoutAsync()
    {
        await _tokenStore.ClearAsync();
        if (_connection.State == HubConnectionState.Connected)
            await _connection.StopAsync();
    }

    // ─── Albums ─────────────────────────────────────────────────

    public async Task<AlbumResponse[]> GetAlbumsAsync(long? categoryId = null, long? cursor = null)
    {
        await EnsureConnected();
        return await _connection.InvokeAsync<AlbumResponse[]>("GetAlbums", categoryId, cursor);
    }

    public async Task<AlbumResponse?> GetAlbumAsync(long id)
    {
        await EnsureConnected();
        return await _connection.InvokeAsync<AlbumResponse?>("GetAlbum", id);
    }

    public async Task<AlbumResponse> CreateAlbumAsync(CreateAlbumRequest request)
    {
        await EnsureConnected();
        return await _connection.InvokeAsync<AlbumResponse>("CreateAlbum", request);
    }

    public async Task UpdateAlbumAsync(long id, UpdateAlbumRequest request)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("UpdateAlbum", id, request);
    }

    public async Task RemoveAlbumAsync(long id)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("RemoveAlbum", id);
    }

    public async Task SubscribeAlbumAsync(long id)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("SubscribeAlbum", id);
    }

    public async Task UnsubscribeAlbumAsync(long id)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("UnsubscribeAlbum", id);
    }

    // ─── Images ──────────────────────────────────────────────────

    public async Task<ImageResponse> AddImageAsync(long albumId, AddImageRequest request)
    {
        await EnsureConnected();
        return await _connection.InvokeAsync<ImageResponse>("AddImage", albumId, request);
    }

    public async Task<ImageResponse[]> GetImagesAsync(long? albumId = null, long? cursor = null)
    {
        await EnsureConnected();
        return await _connection.InvokeAsync<ImageResponse[]>("GetImages", albumId ?? 0, cursor);
    }

    public async Task RemoveImageAsync(long albumId, long imageId)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("RemoveImage", albumId, imageId);
    }

    // ─── Categories ─────────────────────────────────────────────

    public async Task<CategoryResponse[]> GetCategoriesAsync()
    {
        await EnsureConnected();
        return await _connection.InvokeAsync<CategoryResponse[]>("GetCategories");
    }

    public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request)
    {
        await EnsureConnected();
        return await _connection.InvokeAsync<CategoryResponse>("CreateCategory", request);
    }

    public async Task UpdateCategoryAsync(long id, UpdateCategoryRequest request)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("UpdateCategory", id, request);
    }

    public async Task DeleteCategoryAsync(long id)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("DeleteCategory", id);
    }

    // ─── User / Profile ─────────────────────────────────────────

    public async Task<UserProfileResponse?> GetProfileAsync(long? userId = null)
    {
        await EnsureConnected();
        return await _connection.InvokeAsync<UserProfileResponse?>("GetProfile", userId);
    }

    public async Task UpdateProfileAsync(UpdateProfileRequest request)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("UpdateProfile", request);
    }

    public async Task UpdateAvatarAsync(byte[] fileBytes)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("UpdateAvatar", fileBytes);
    }

    public async Task UpdateHeaderAsync(byte[] fileBytes)
    {
        await EnsureConnected();
        await _connection.InvokeAsync("UpdateHeader", fileBytes);
    }

    // ─── Dispose ─────────────────────────────────────────────────

    public async ValueTask DisposeAsync()
    {
        if (_ownsConnection)
            await _connection.DisposeAsync();
    }
}
