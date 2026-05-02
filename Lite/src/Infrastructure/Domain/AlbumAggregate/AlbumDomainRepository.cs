using Domain.AlbumAggregate.AlbumEntity;
using Domain.Database;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Domain.AlbumAggregate;

internal sealed class AlbumDomainRepository(DomainDbContext context) : IAlbumRepository
{
    private readonly DomainDbContext _context = context;

    public async Task AddAsync(Album entity, CancellationToken cancellationToken = default)
    {
        await _context.Albums.AddAsync(entity, cancellationToken);
    }

    public async Task<Album> GetAsync(AlbumId id, CancellationToken cancellationToken = default)
    {
        var album =
            await _context.Albums.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            ?? throw new EntityNotFoundException();

        return album;
    }
}
