using Domain.AlbumAggregate.Exceptions;
using Domain.CategoryAggregate.CategoryEntity;
using Domain.Database;
using Microsoft.EntityFrameworkCore;

namespace Domain.AlbumAggregate.Services;

internal sealed class CategoryExistenceChecker(DomainDbContext context) : ICategoryExistenceChecker
{
    private readonly DomainDbContext _context = context;

    public async Task CheckAsync(CategoryId category, CancellationToken cancellationToken = default)
    {
        bool isExisting = await _context
            .Categories.AsNoTracking()
            .AnyAsync(c => c.Id == category, cancellationToken);

        if (isExisting == false)
        {
            CategoryNotFoundException.Throw(category);
        }
    }
}
