using Domain;
using Mediator;

namespace Query.Album;

[Alias(nameof(AlbumDto))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed class AlbumDto
{
    public required long Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required long Author { get; init; }
    public required long Category { get; init; }
    public required string[] Tags { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required int SubscribeCount { get; init; }
}

[Alias(nameof(AlbumsQuery))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed record class AlbumsQuery(
    long? CategoryId,
    long? AuthorId,
    string? Title,
    long? Cursor,
    Actor Actor
) : IQuery<AlbumDto[]>;
