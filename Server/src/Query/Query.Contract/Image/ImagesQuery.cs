using Domain;
using Mediator;

namespace Query.Image;

[Alias(nameof(ImageDto))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public readonly record struct ImageDto
{
    public required long Id { get; init; }
    public required long AlbumId { get; init; }
    public required long UploaderId { get; init; }
    public required string Title { get; init; }
    public required DateTime UploadedAt { get; init; }
    public required string[] Tags { get; init; }
    public required int Likes { get; init; }
    public required RequesterInfo Requester { get; init; }

    public readonly record struct RequesterInfo(bool Liked);
}

[Alias(nameof(ImagesQuery))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed record class ImagesQuery(long? AuthorId, long? AlbumId, long? Cursor, Actor Actor)
    : IQuery<ImageDto[]>;
