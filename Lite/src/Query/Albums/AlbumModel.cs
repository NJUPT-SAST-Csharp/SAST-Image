using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.Events;
using Query.Images;

namespace Query.Albums;

public sealed class AlbumModel
{
    [Obsolete("For ORM", true)]
    private AlbumModel() { }

    internal AlbumModel(AlbumCreatedEvent e)
    {
        Id = e.AlbumId.Value;
        Title = e.Title.Value;
        Description = e.Description.Value;
        AuthorId = e.AuthorId.Value;
        CategoryId = e.CategoryId.Value;
        AccessLevel = e.AccessLevel.Value;
    }

    public long Id { get; }
    public string Title { get; internal set; } = null!;
    public string Description { get; internal set; } = null!;
    public long AuthorId { get; private set; }
    public long CategoryId { get; internal set; }
    public AccessLevelValue AccessLevel { get; private set; }
    public string[] Tags { get; internal set; } = [];
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; internal set; } = DateTime.UtcNow;
    public DateTime? RemovedAt { get; private set; }
    public List<SubscribeModel> Subscribes { get; } = null!;
    public List<ImageModel> Images { get; } = null!;

    internal void UpdateAccessLevel(AlbumAccessLevelUpdatedEvent e)
    {
        AccessLevel = e.AccessLevel.Value;
        foreach (var image in Images)
        {
            image.AccessLevel = e.AccessLevel.Value;
        }
    }

    internal void Remove(AlbumRemovedEvent e)
    {
        RemovedAt = DateTime.UtcNow;

        foreach (var image in Images)
        {
            image.AlbumRemoved(e);
        }
    }

    internal void Restore(AlbumRestoredEvent e)
    {
        RemovedAt = null;

        foreach (var image in Images)
        {
            image.AlbumRestored(e);
        }
    }
}

public sealed record class SubscribeModel(long Album, long User);
