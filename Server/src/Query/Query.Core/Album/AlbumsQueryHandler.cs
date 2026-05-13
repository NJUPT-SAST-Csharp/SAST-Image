using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Album;

public sealed class AlbumsQueryHandler(QueryDbContext context)
    : IQueryHandler<AlbumsQuery, AlbumDto[]>
{
    public const int PageSize = 60;

    private static readonly Func<
        QueryDbContext,
        long?,
        long?,
        string?,
        long?,
        long,
        bool,
        bool,
        IAsyncEnumerable<AlbumDto>
    > Query = EF.CompileAsyncQuery(
        (
            QueryDbContext context,
            long? categoryId,
            long? authorId,
            string? title,
            long? cursor,
            long actorId,
            bool isAuthenticated,
            bool isAdmin
        ) =>
            context
                .Albums.AsNoTracking()
                .Where(a => categoryId == null || a.CategoryId == categoryId)
                .Where(a => authorId == null || a.AuthorId == authorId)
                .Where(a => title == null || EF.Functions.ILike(a.Title, "%" + title + "%"))
                .Where(i => cursor == null || i.Id < cursor)
                .OrderByDescending(a => a.Id)
                .Take(PageSize)
                .Select(a => new AlbumDto()
                {
                    Author = a.AuthorId,
                    Category = a.CategoryId,
                    UpdatedAt = a.UpdatedAt,
                    CreatedAt = a.CreatedAt,
                    Id = a.Id,
                    Tags = a.Tags,
                    Title = a.Title,
                    Description = a.Description,
                    SubscribeCount = a.Subscribes.Length,
                })
    );

    public async ValueTask<AlbumDto[]> Handle(
        AlbumsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await Query(
                context,
                request.CategoryId,
                request.AuthorId,
                request.Title,
                request.Cursor,
                request.Actor.Id.Value,
                request.Actor.IsAuthenticated,
                request.Actor.IsAdmin
            )
            .ToArrayAsync(cancellationToken);
    }
}
