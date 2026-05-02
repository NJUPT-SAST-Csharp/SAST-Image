using Domain.AlbumAggregate.AlbumEntity;
using Domain.Shared;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Albums.Queries;

public sealed class RemovedAlbumDto
{
    public required long Id { get; init; }
    public required string Title { get; init; }
    public required long Category { get; init; }
    public required AccessLevelValue AccessLevel { get; init; }
    public required DateTime RemovedAt { get; init; }
}

public sealed record class RemovedAlbumsQuery(Actor Actor) : IQuery<RemovedAlbumDto[]>
{
    public static readonly Func<
        QueryDbContext,
        long,
        bool,
        IAsyncEnumerable<RemovedAlbumDto>
    > Query = EF.CompileAsyncQuery(
        (QueryDbContext context, long actorId, bool isAdmin) =>
            context
                .Albums.AsNoTracking()
                .Where(a => a.RemovedAt != null)
                .Where(a => a.AuthorId == actorId || isAdmin)
                .Select(a => new RemovedAlbumDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Category = a.CategoryId,
                    AccessLevel = a.AccessLevel,
                    RemovedAt = a.RemovedAt.GetValueOrDefault(),
                })
    );
}

public sealed class RemovedAlbumsQueryHandler(QueryDbContext context)
    : IQueryHandler<RemovedAlbumsQuery, RemovedAlbumDto[]>
{
    public async ValueTask<RemovedAlbumDto[]> Handle(
        RemovedAlbumsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await RemovedAlbumsQuery
            .Query(context, request.Actor.Id, request.Actor.IsAdmin)
            .ToArrayAsync(cancellationToken);
    }
}
