using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Category;

public sealed class CategoriesQueryHandler(QueryDbContext context)
    : IQueryHandler<CategoriesQuery, CategoryDto[]>
{
    private static readonly Func<QueryDbContext, long?, IAsyncEnumerable<CategoryDto>> Query =
        EF.CompileAsyncQuery(
            (QueryDbContext context, long? categoryId) =>
                context
                    .Categories.Where(c => categoryId == null || c.Id == categoryId)
                    .Select(c => new CategoryDto(c.Id, c.Name, c.Description))
        );

    public async ValueTask<CategoryDto[]> Handle(
        CategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await Query(context, request.Id).ToArrayAsync(cancellationToken);
    }
}
