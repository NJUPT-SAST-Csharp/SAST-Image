using Domain.Api;

namespace App.Models;

public sealed class AlbumModel
{
    public long Id { get; init; }
    public string Title { get; init; } = "";
    public string Description { get; init; } = "";
    public long AuthorId { get; init; }
    public string AuthorName { get; init; } = "";
    public long CategoryId { get; init; }
    public string CategoryName { get; init; } = "";
    public string[] Tags { get; init; } = [];
    public int SubscribeCount { get; init; }
    public AccessLevel AccessLevel { get; init; }
    public long CreatedAt { get; init; }
    public long UpdatedAt { get; init; }

    public static implicit operator AlbumModel(AlbumResponse r) => new()
    {
        Id = r.Id,
        Title = r.Title,
        Description = r.Description,
        AuthorId = r.AuthorId,
        AuthorName = r.AuthorName,
        CategoryId = r.CategoryId,
        CategoryName = r.CategoryName,
        Tags = r.Tags,
        SubscribeCount = r.SubscribeCount,
        AccessLevel = r.AccessLevel,
        CreatedAt = r.CreatedAt,
        UpdatedAt = r.UpdatedAt,
    };
}
