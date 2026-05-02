using Domain.AlbumAggregate.AlbumEntity;
using Domain.Shared;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Albums.Queries;

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
    public required AccessLevelValue AccessLevel { get; init; }
    public required int SubscribeCount { get; init; }
}

public sealed record class AlbumsQuery(
    long? CategoryId,
    long? AuthorId,
    string? Title,
    long? Cursor,
    Actor Actor
) : IQuery<AlbumDto[]>
{
    public const int PageSize = 60;

    public static readonly Func<
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
                .Where(a => a.RemovedAt == null)
                .Where(a => categoryId == null || a.CategoryId == categoryId)
                .Where(a => authorId == null || a.AuthorId == authorId)
                .Where(a => title == null || EF.Functions.ILike(a.Title, "%" + title + "%"))
                .Where(a =>
                    a.AccessLevel >= AccessLevelValue.PublicReadOnly
                    || a.AccessLevel >= AccessLevelValue.AuthReadOnly && isAuthenticated
                    || a.AccessLevel == AccessLevelValue.Private
                        && (a.AuthorId == actorId || isAdmin)
                )
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
                    AccessLevel = a.AccessLevel,
                    SubscribeCount = a.Subscribes.Count,
                })
    );
}

public sealed class AlbumsQueryHandler(QueryDbContext context)
    : IQueryHandler<AlbumsQuery, AlbumDto[]>
{
    public async ValueTask<AlbumDto[]> Handle(
        AlbumsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await AlbumsQuery
            .Query(
                context,
                request.CategoryId,
                request.AuthorId,
                request.Title,
                request.Cursor,
                request.Actor.Id,
                request.Actor.IsAuthenticated,
                request.Actor.IsAdmin
            )
            .ToArrayAsync(cancellationToken);
    }
}
