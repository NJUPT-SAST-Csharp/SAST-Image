using Domain.Category.Events;
using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Categories.EventHandlers;

public sealed class CategoryDeletedEventHandler(QueryDbContext context)
    : IDomainEventHandler<CategoryDeletedEvent>
{
    public async ValueTask Handle(CategoryDeletedEvent e, CancellationToken cancellationToken)
    {
        await context.Categories.Where(c => c.Id == e.Id).ExecuteDeleteAsync(cancellationToken);
    }
}
