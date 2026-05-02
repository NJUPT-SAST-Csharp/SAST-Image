using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.ImageEntity;
using Domain.Shared;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Images.Queries;

public sealed record RemovedImagesQuery(AlbumId Album, Actor Actor) : IQuery<ImageDto[]>
{
    internal static readonly Func<
        QueryDbContext,
        long,
        long,
        bool,
        IAsyncEnumerable<ImageDto>
    > Query = EF.CompileAsyncQuery(
        (QueryDbContext context, long albumId, long actorId, bool isAdmin) =>
            context
                .Images.AsNoTracking()
                .Where(i => i.Status == ImageStatusValue.Removed)
                .Where(i => i.AlbumId == albumId)
                .Where(i => i.AuthorId == actorId || isAdmin)
                .Select(i => new ImageDto
                {
                    Id = i.Id,
                    UploaderId = i.UploaderId,
                    AlbumId = i.AlbumId,
                    Title = i.Title,
                    Tags = i.Tags,
                    UploadedAt = i.UploadedAt,
                    Likes = i.Likes.Count,
                    Requester = new(i.Likes.Select(l => l.User).Contains(actorId)),
                })
    );
}

public sealed class RemovedImagesQueryHandler(QueryDbContext context)
    : IQueryHandler<RemovedImagesQuery, ImageDto[]>
{
    public async ValueTask<ImageDto[]> Handle(
        RemovedImagesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await RemovedImagesQuery
            .Query(context, request.Album.Value, request.Actor.Id, request.Actor.IsAdmin)
            .ToArrayAsync(cancellationToken);
    }
}
