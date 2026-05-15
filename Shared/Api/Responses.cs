using UserId = long;
using AlbumId = long;
using ImageId = long;
using CategoryId = long;

namespace Domain.Api;

public enum AccessLevel : byte
{
    Private = 0,
    AuthReadOnly = 1,
    AuthReadWrite = 2,
    PublicReadOnly = 3,
    PublicReadWrite = 4,
}

public sealed record JwtTokenResponse(
    string AccessToken,
    string RefreshToken,
    long ExpireIn);

public sealed record AlbumResponse(
    AlbumId Id,
    string Title,
    string Description,
    UserId AuthorId,
    string AuthorName,
    CategoryId CategoryId,
    string CategoryName,
    string[] Tags,
    int SubscribeCount,
    AccessLevel AccessLevel,
    long CreatedAt,
    long UpdatedAt);

public sealed record AlbumListResponse(
    AlbumResponse[] Albums,
    string? NextCursor);

public sealed record ImageResponse(
    ImageId Id,
    AlbumId AlbumId,
    string Title,
    UserId UploaderId,
    string UploaderName,
    string[] Tags,
    int Likes,
    bool Liked,
    long UploadedAt,
    string? ThumbnailUrl = null);

public sealed record CategoryResponse(
    CategoryId Id,
    string Name,
    string Description);

public sealed record UserProfileResponse(
    UserId Id,
    string Username,
    string Nickname,
    string Biography,
    long RegisteredAt);

public sealed record ErrorResponse(
    string Message,
    int Code);
