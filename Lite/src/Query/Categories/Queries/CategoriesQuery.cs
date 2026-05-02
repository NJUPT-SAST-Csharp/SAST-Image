using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Categories.Queries;

public record class CategoryDto(long Id, string Name, string Description);

public record class CategoriesQuery() : IQuery<CategoryDto[]>
{
    internal static readonly Func<QueryDbContext, IAsyncEnumerable<CategoryDto>> Query =
        EF.CompileAsyncQuery(
            (QueryDbContext context) =>
                context.Categories.Select(c => new CategoryDto(c.Id, c.Name, c.Description))
        );
}

public sealed class CategoriesQueryHandler(QueryDbContext context)
    : IQueryHandler<CategoriesQuery, CategoryDto[]>
{
    public async ValueTask<CategoryDto[]> Handle(
        CategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = CategoriesQuery.Query(context);

        return await result.ToArrayAsync(cancellationToken);
    }
}
