using Domain.AlbumAggregate.AlbumEntity;
using Domain.Shared;

namespace Storage.Albums;

public interface IAlbumAvailabilityChecker
{
    public Task<bool> CheckAsync(AlbumId albumId, Actor actor, CancellationToken cancellationToken);
}
