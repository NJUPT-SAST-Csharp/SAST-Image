using Domain;
using Domain.Album;
using Mediator;
using Storage.Services;

namespace Storage.Album.Queries;

public sealed record class AlbumCoverQuery(AlbumId Id, Actor Actor) : IQuery<Stream?>;

public sealed class AlbumCoverQueryHandler(LocalImageFileManager manager, IAccessChecker checker)
    : IQueryHandler<AlbumCoverQuery, Stream?>
{
    public async ValueTask<Stream?> Handle(
        AlbumCoverQuery request,
        CancellationToken cancellationToken
    )
    {
        bool available = await checker.HasAccessAsync(request.Actor, request.Id, cancellationToken);

        if (available is false)
            return null;
        return manager.GetStream(request.Id);
    }
}
