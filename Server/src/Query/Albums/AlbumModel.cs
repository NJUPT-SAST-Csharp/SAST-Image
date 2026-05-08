global using AlbumId = long;
using Domain.Album.Events;
using Query.Images;

namespace Query.Albums;

public sealed class AlbumModel
{
    [Obsolete("For ORM", true)]
    private AlbumModel() { }

    internal AlbumModel(AlbumCreatedEvent e)
    {
        Id = e.Id.Value;
        Title = e.Title.Value;
        Description = e.Description.Value;
        AuthorId = e.Actor.Id.Value;
        CategoryId = e.CategoryId.Value;
    }

    public AlbumId Id { get; }
    public string Title { get; internal set; } = null!;
    public string Description { get; internal set; } = null!;
    public UserId AuthorId { get; private set; }
    public CategoryId CategoryId { get; internal set; }
    public string[] Tags { get; internal set; } = [];
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; internal set; } = DateTime.UtcNow;
    public UserId[] Subscribes { get; internal set; } = [];
    public List<ImageModel> Images { get; } = [];
}
