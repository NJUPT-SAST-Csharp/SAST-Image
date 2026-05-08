using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Query.Database;

public static class QueryExtensions
{
    extension<TEntity>(IQueryable<TEntity> entities)
        where TEntity : class
    {
        public async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken
        )
        {
            return await entities.FirstOrDefaultAsync(predicate, cancellationToken)
                ?? throw new EntityNotFoundException();
        }
    }
}
