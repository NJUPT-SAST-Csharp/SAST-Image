using System.Security.Claims;
using Domain;
using Domain.Album;
using Domain.Album.Image;
using Domain.Api;
using Domain.Category;
using Domain.User;
using Domain.File;
using WebApi.Services;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Orleans.Concurrency;
using Orleans.Runtime;
using Query.Album;
using Query.Category;
using Query.Image;
using Query.User;

using AlbumGrain = Domain.Album.IAlbumGrain;
using CategoryGrain = Domain.Category.ICategoryGrain;
using UserGrain = Domain.User.IUserGrain;
using AlbumId = Domain.Album.AlbumId;
using AlbumTitle = Domain.Album.AlbumTitle;
using AlbumDescription = Domain.Album.AlbumDescription;
using AlbumTags = Domain.Album.AlbumTags;
using DomainImageId = Domain.Album.Image.ImageId;
using DomainImageDescription = Domain.Album.Image.ImageDescription;
using DomainImageTags = Domain.Album.Image.ImageTags;
using DomainCategoryId = Domain.Category.CategoryId;
using CategoryName = Domain.Category.CategoryName;
using CategoryDescription = Domain.Category.CategoryDescription;
using DomainUserId = Domain.User.UserId;
using Username = Domain.User.Username;
using Nickname = Domain.User.Nickname;
using Biography = Domain.User.Biography;
using ImageFileKey = Domain.File.ImageFileKey;

namespace WebApi.Hubs;

public class MainHub : Hub
{
    private readonly IGrainFactory _grains;
    private readonly IMediator _mediator;
    private readonly JwtTokenService _jwt;

    public MainHub(
        IGrainFactory grains,
        IMediator mediator,
        JwtTokenService jwt)
    {
        _grains = grains;
        _mediator = mediator;
        _jwt = jwt;
    }

    // ─── Account ────────────────────────────────────────────────

    public async Task<JwtTokenResponse> Login(LoginRequest request)
    {
        var profile = await _mediator.Send(
            new UserByUsernameQuery(new Username(request.Username)));
        if (profile is null)
            throw new HubException("User not found");

        // FIXME: All users get Admin role for bootstrapping.
        // Should read the user's actual role from storage.
        var token = _jwt.Generate(new UserId(profile.Id), new Username(profile.Username), Role.Admin);
        return new JwtTokenResponse(token.AccessToken, token.RefreshToken, token.ExpireIn);
    }

    public async Task<JwtTokenResponse> Register(RegisterRequest request)
    {
        SetActor(null);
        var userId = UserId.GenerateNew();
        var grain = _grains.GetGrain<IUserGrain>(userId.Value);
        var registeredId = await grain.Register(
            new Username(request.Username),
            new Nickname(request.Nickname),
            new Biography(request.Biography));

        if (registeredId is null)
            throw new HubException("Registration failed: username may already be taken");

        // FIXME: First registered user gets Admin role for bootstrapping.
        // Should be replaced with a proper admin seeding mechanism.
        var token = _jwt.Generate(registeredId.Value, new Username(request.Username), Role.Admin);
        return new JwtTokenResponse(token.AccessToken, token.RefreshToken, token.ExpireIn);
    }

    public JwtTokenResponse RefreshToken(string refreshToken)
    {
        var (userId, isValid) = _jwt.DecodeRefreshToken(refreshToken);
        if (!isValid)
            throw new HubException("Invalid or expired refresh token");

        var username = Context.User?.FindFirst("username")?.Value ?? "unknown";
        var role = Context.User?.FindFirst("role")?.Value;
        var userRole = role == ((byte)Role.Admin).ToString() ? Role.Admin : Role.User;

        var token = _jwt.Generate(userId, new Username(username), userRole);
        return new JwtTokenResponse(token.AccessToken, token.RefreshToken, token.ExpireIn);
    }

    // ─── Albums ─────────────────────────────────────────────────

    [Authorize]
    public async Task<AlbumResponse[]> GetAlbums(long? categoryId, long? cursor)
    {
        var actor = GetActor();
        var albums = await _mediator.Send(new AlbumsQuery(categoryId, null, null, cursor, actor));
        return await ToAlbumResponses(albums);
    }

    [Authorize]
    public async Task<AlbumResponse?> GetAlbum(long id)
    {
        var actor = GetActor();
        var albums = await _mediator.Send(new AlbumsQuery(null, null, null, null, actor));
        var album = albums.FirstOrDefault(a => a.Id == id);
        if (album is null) return null;

        var responses = await ToAlbumResponses([album]);
        return responses[0];
    }

    [Authorize]
    public async Task<AlbumResponse> CreateAlbum(CreateAlbumRequest request)
    {
        var actor = GetActor();
        SetActor(actor);
        var albumId = AlbumId.GenerateNew();
        var grain = _grains.GetGrain<IAlbumGrain>(albumId.Value);
        albumId = await grain.Create(
            new AlbumTitle(request.Title),
            new AlbumDescription(request.Description),
            new AlbumTags(request.Tags),
            new CategoryId(request.CategoryId));

        return new AlbumResponse(
            albumId.Value, request.Title, request.Description,
            actor.Id.Value, "unknown", request.CategoryId, "unknown",
            request.Tags, 0, AccessLevel.PublicReadWrite,
            DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }

    [Authorize]
    public async Task UpdateAlbum(long id, UpdateAlbumRequest request)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<IAlbumGrain>(id);
        await grain.Update(
            request.Title is not null ? new AlbumTitle(request.Title) : null,
            request.Description is not null ? new AlbumDescription(request.Description) : null,
            request.Tags is not null ? new AlbumTags(request.Tags) : null,
            request.CategoryId is not null ? new CategoryId(request.CategoryId.Value) : null);
    }

    [Authorize]
    public async Task RemoveAlbum(long id)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<IAlbumGrain>(id);
        await grain.Remove();
    }

    [Authorize]
    public async Task SubscribeAlbum(long id)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<IAlbumGrain>(id);
        await grain.Subscribe();
    }

    [Authorize]
    public async Task UnsubscribeAlbum(long id)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<IAlbumGrain>(id);
        await grain.Unsubscribe();
    }

    // ─── Images ──────────────────────────────────────────────────

    [Authorize]
    public async Task<ImageResponse> AddImage(long albumId, AddImageRequest request)
    {
        var actor = GetActor();
        SetActor(actor);

        var imageId = ImageId.GenerateNew();

        var fileManager = _grains.GetGrain<IFileSyncGrain>(Guid.Empty);
        var fileKey = await fileManager.UploadAsync(request.FileBytes.AsImmutable(), CancellationToken.None);

        var grain = _grains.GetGrain<IAlbumGrain>(albumId);
        await grain.AddImage(
            imageId,
            new ImageDescription(request.Title),
            new ImageTags(request.Tags),
            fileKey);

        return new ImageResponse(
            imageId.Value, albumId, request.Title, actor.Id.Value, "unknown",
            request.Tags, 0, false,
            DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            $"/images/{imageId.Value}");
    }

    [Authorize]
    public async Task RemoveImage(long albumId, long imageId)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<IAlbumGrain>(albumId);
        await grain.RemoveImage(new ImageId(imageId));
    }

    [Authorize]
    public async Task<ImageResponse[]> GetImages(long albumId, long? cursor)
    {
        var actor = GetActor();
        var images = await _mediator.Send(new ImagesQuery(null, albumId, cursor, actor));
        if (images.Length == 0) return [];

        var uploaderIds = images.Select(i => i.UploaderId).Distinct();
        var userNames = new Dictionary<long, string>();
        foreach (var uid in uploaderIds)
        {
            var profile = await _mediator.Send(new UserProfileQuery(new UserId(uid)));
            userNames[uid] = profile?.Username ?? "unknown";
        }

        return images.Select(i => new ImageResponse(
            i.Id, i.AlbumId, i.Title, i.UploaderId, userNames.GetValueOrDefault(i.UploaderId, "unknown"),
            i.Tags, 0, false,
            new DateTimeOffset(i.UploadedAt).ToUnixTimeSeconds(),
            $"/images/{i.Id}")).ToArray();
    }

    // ─── Categories ─────────────────────────────────────────────

    public async Task<CategoryDto[]> GetCategories()
    {
        return await _mediator.Send(new CategoriesQuery());
    }

    [Authorize]
    public async Task<CategoryDto> CreateCategory(CreateCategoryRequest request)
    {
        var actor = GetActor();
        SetActor(actor);
        var categoryId = CategoryId.GenerateNew();
        var grain = _grains.GetGrain<ICategoryGrain>(categoryId.Value);
        var id = await grain.Create(
            new CategoryName(request.Name),
            new CategoryDescription(request.Description));
        return new CategoryDto(categoryId.Value, request.Name, request.Description);
    }

    [Authorize]
    public async Task UpdateCategory(long id, UpdateCategoryRequest request)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<ICategoryGrain>(id);
        await grain.Update(
            request.Name is not null ? new CategoryName(request.Name) : null,
            request.Description is not null ? new CategoryDescription(request.Description) : null);
    }

    [Authorize]
    public async Task DeleteCategory(long id)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<ICategoryGrain>(id);
        await grain.Delete();
    }

    // ─── User / Profile ─────────────────────────────────────────

    [Authorize]
    public async Task<UserProfileResponse?> GetProfile(long? userId)
    {
        var actor = GetActor();
        var id = userId.HasValue ? userId.Value : actor.Id.Value;
        var dto = await _mediator.Send(new UserProfileQuery(id));

        if (dto is null) return null;

        return new UserProfileResponse(
            dto.Id, dto.Username, dto.Nickname, dto.Biography, 0);
    }

    [Authorize]
    public async Task UpdateProfile(UpdateProfileRequest request)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<IUserGrain>(actor.Id.Value);

        if (request.Username is not null)
            await grain.UpdateUsername(new Username(request.Username));

        await grain.UpdateProfile(
            request.Nickname is not null ? new Nickname(request.Nickname) : null,
            request.Biography is not null ? new Biography(request.Biography) : null);
    }

    [Authorize]
    public async Task UpdateAvatar(byte[] fileBytes)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<IUserGrain>(actor.Id.Value);
        await grain.UpdateAvatar(fileBytes.AsImmutable());
    }

    [Authorize]
    public async Task UpdateHeader(byte[] fileBytes)
    {
        var actor = GetActor();
        SetActor(actor);
        var grain = _grains.GetGrain<IUserGrain>(actor.Id.Value);
        await grain.UpdateHeader(fileBytes.AsImmutable());
    }

    // ─── Helpers ─────────────────────────────────────────────────

    private Actor GetActor()
    {
        if (Context.User?.Identity?.IsAuthenticated != true)
            throw new HubException("Not authenticated");

        var userIdClaim = Context.User.FindFirst("id")?.Value;
        var usernameClaim = Context.User.FindFirst("username")?.Value;
        var roleClaim = Context.User.FindFirst("role")?.Value;

        if (userIdClaim is null || !long.TryParse(userIdClaim, out var uid))
            throw new HubException("Invalid token: missing user id");

        return new Actor
        {
            Id = new UserId(uid),
            IsAuthenticated = true,
            Role = roleClaim == ((byte)Role.Admin).ToString() ? Role.Admin : Role.User,
        };
    }

    private static void SetActor(Actor? actor)
    {
        RequestContext.Set("Actor", actor ?? new Actor { Id = new UserId(0), IsAuthenticated = false, Role = Role.None });
    }

    private async Task<AlbumResponse[]> ToAlbumResponses(AlbumDto[] albums)
    {
        if (albums.Length == 0) return [];

        var allCategories = await _mediator.Send(new CategoriesQuery(null));
        var categoryNames = allCategories.ToDictionary(c => c.Id, c => c.Name);

        var userIds = albums.Select(a => a.Author).Distinct();
        var userNames = new Dictionary<long, string>();
        foreach (var uid in userIds)
        {
            var profile = await _mediator.Send(new UserProfileQuery(new UserId(uid)));
            userNames[uid] = profile?.Username ?? "unknown";
        }

        return albums.Select(a => new AlbumResponse(
            a.Id, a.Title, a.Description,
            a.Author, userNames.GetValueOrDefault(a.Author, "unknown"),
            a.Category, categoryNames.GetValueOrDefault(a.Category, "unknown"),
            a.Tags, a.SubscribeCount, AccessLevel.PublicReadWrite,
            new DateTimeOffset(a.CreatedAt).ToUnixTimeSeconds(),
            new DateTimeOffset(a.UpdatedAt).ToUnixTimeSeconds()
        )).ToArray();
    }
}
