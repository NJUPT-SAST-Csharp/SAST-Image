using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Image;

public sealed class ImagesQueryHandler(QueryDbContext context)
    : IQueryHandler<ImagesQuery, ImageDto[]>
{
    const int PageSize = 50;

    private static readonly Func<
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

    public async ValueTask<ImageDto[]> Handle(
        ImagesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await Query(
                context,
                request.AuthorId,
                request.AlbumId,
                request.Cursor,
                request.Actor.Id.Value,
                request.Actor.IsAuthenticated,
                request.Actor.IsAdmin
            )
            .ToArrayAsync(cancellationToken);
    }
}
