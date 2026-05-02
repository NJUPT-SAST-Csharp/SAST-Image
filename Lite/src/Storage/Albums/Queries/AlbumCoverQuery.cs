using Domain.AlbumAggregate.AlbumEntity;
using Domain.Shared;
using Mediator;

namespace Storage.Albums.Queries;

public sealed record class AlbumCoverQuery(AlbumId Id, Actor Actor) : IQuery<ImageFile?>;

public sealed class AlbumCoverQueryHandler(
    IImageFileManager manager,
    IAlbumAvailabilityChecker checker
) : IQueryHandler<AlbumCoverQuery, ImageFile?>
{
    public async ValueTask<ImageFile?> Handle(
        AlbumCoverQuery request,
        CancellationToken cancellationToken
    )
    {
        bool available = await checker.CheckAsync(request.Id, request.Actor, cancellationToken);

        if (available == false)
            return null;
        if (manager.TryGet(request.Id, out var file) is false)
            return null;

        return file;
    }
}
