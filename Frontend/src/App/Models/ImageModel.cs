using Domain.Api;

namespace App.Models;

public sealed class ImageModel
{
    public long Id { get; init; }
    public long AlbumId { get; init; }
    public string Title { get; init; } = "";
    public long UploaderId { get; init; }
    public string UploaderName { get; init; } = "";
    public string[] Tags { get; init; } = [];
    public int Likes { get; init; }
    public bool Liked { get; init; }
    public long UploadedAt { get; init; }
    public string? ThumbnailUrl { get; init; }

    public static implicit operator ImageModel(ImageResponse r) => new()
    {
        Id = r.Id,
        AlbumId = r.AlbumId,
        Title = r.Title,
        UploaderId = r.UploaderId,
        UploaderName = r.UploaderName,
        Tags = r.Tags,
        Likes = r.Likes,
        Liked = r.Liked,
        UploadedAt = r.UploadedAt,
        ThumbnailUrl = r.ThumbnailUrl,
    };
}
