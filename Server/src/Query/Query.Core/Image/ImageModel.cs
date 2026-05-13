using Domain.Album.Events;

namespace Query.Image;

public sealed class ImageModel
{
    [Obsolete("For ORM", true)]
    private ImageModel() { }

    public long Id { get; }
    public string Title { get; internal set; } = null!;
    public long AlbumId { get; }
    public long AuthorId { get; }
    public long UploaderId { get; }
    public string[] Tags { get; internal set; } = [];
    public DateTime UploadedAt { get; } = DateTime.UtcNow;
    public long[] Likes { get; internal set; } = null!;

    internal ImageModel(AlbumImageAddedEvent e)
    {
        Id = e.ImageId.Value;
        AlbumId = e.Id.Value;
        Title = e.Title.Value;
        Tags = e.Tags.Value;
        UploadedAt = e.Timestamp;
        UploaderId = e.Uploader.Id.Value;
    }
}
