using Domain.Category.Events;
using Domain.Event;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Category.EventHandlers;

public sealed class CategoryDeletedEventHandler(QueryDbContext context)
    : IDomainEventHandler<CategoryDeletedEvent>
{
    public async ValueTask<Unit> Handle(CategoryDeletedEvent e, CancellationToken cancellationToken)
    {
        await context.Categories.Where(c => c.Id == e.Id).ExecuteDeleteAsync(cancellationToken);

        return Unit.Value;
    }
}
