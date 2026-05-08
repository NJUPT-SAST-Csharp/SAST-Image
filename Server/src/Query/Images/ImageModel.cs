global using ImageId = long;
using Domain.Album.Events;

namespace Query.Images;

public sealed class ImageModel
{
    [Obsolete("For ORM", true)]
    private ImageModel() { }

    public ImageId Id { get; }
    public string Title { get; internal set; } = null!;
    public AlbumId AlbumId { get; }
    public UserId AuthorId { get; }
    public UserId UploaderId { get; }
    public string[] Tags { get; internal set; } = [];
    public DateTime UploadedAt { get; } = DateTime.UtcNow;
    public UserId[] Likes { get; internal set; } = null!;

    internal ImageModel(AlbumImageAddedEvent e)
    {
        Id = e.ImageId;
        AlbumId = e.Id.Value;
        Title = e.Title.Value;
        Tags = e.Tags.Value;
        UploadedAt = e.Timestamp;
        UploaderId = e.Uploader.Id;
    }
}
