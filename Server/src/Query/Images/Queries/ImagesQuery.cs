using Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Images.Queries;

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

public sealed record class ImagesQuery(long? AuthorId, long? AlbumId, long? Cursor, Actor Actor)
    : IQuery<ImageDto[]>
{
    const int PageSize = 50;

    internal static readonly Func<
        QueryDbContext,
        long?,
        long?,
        long?,
        long,
        bool,
        bool,
        IAsyncEnumerable<ImageDto>
    > Query = EF.CompileAsyncQuery(
        (
            QueryDbContext context,
            long? authorId,
            long? albumId,
            long? cursor,
            long actorId,
            bool isAuthenticated,
            bool isAdmin
        ) =>
            context
                .Images.AsNoTracking()
                .Where(i => authorId == null || i.UploaderId == authorId.Value)
                .Where(i => albumId == null || i.AlbumId == albumId.Value)
                .Where(i => cursor == null || i.Id < cursor)
                .OrderByDescending(i => i.Id)
                .Take(PageSize)
                .Select(i => new ImageDto
                {
                    Id = i.Id,
                    UploaderId = i.UploaderId,
                    AlbumId = i.AlbumId,
                    Title = i.Title,
                    Tags = i.Tags,
                    UploadedAt = i.UploadedAt,
                    Likes = i.Likes.Length,
                    Requester = new(i.Likes.Contains(actorId)),
                })
    );
}

public sealed class ImagesQueryHandler(QueryDbContext context)
    : IQueryHandler<ImagesQuery, ImageDto[]>
{
    public async ValueTask<ImageDto[]> Handle(
        ImagesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await ImagesQuery
            .Query(
                context,
                request.AuthorId,
                request.AlbumId,
                request.Cursor,
                request.Actor.Id,
                request.Actor.IsAuthenticated,
                request.Actor.IsAdmin
            )
            .ToArrayAsync(cancellationToken);
    }
}
