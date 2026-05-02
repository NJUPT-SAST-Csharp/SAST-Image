using Domain.AlbumAggregate.AlbumEntity;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Storage.Albums;

internal sealed class AlbumAvailabilityChecker(QueryDbContext context) : IAlbumAvailabilityChecker
{
    private readonly QueryDbContext _context = context;

    public async Task<bool> CheckAsync(
        AlbumId albumId,
        Actor actor,
        CancellationToken cancellationToken
    )
    {
        var album = await _context
            .Albums.AsNoTracking()
            .Where(a => a.Id == albumId.Value)
            .Select(a => new
            {
                a.AccessLevel,
                a.AuthorId,
                a.RemovedAt,
            })
            .FirstOrDefaultAsync(cancellationToken);

        long actorId = actor.Id.Value;

        if (album is null)
            return false;
        if (actor.IsAdmin || album.AuthorId == actor.Id.Value)
            return true;
        if (album.RemovedAt is null)
            return false;

        return album.AccessLevel switch
        {
            >= AccessLevelValue.PublicReadOnly => true,
            >= AccessLevelValue.AuthReadOnly => actor.IsAuthenticated,
            AccessLevelValue.Private => false,
        };
    }
}
