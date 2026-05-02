using Domain.CategoryAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Categories.EventHandlers;

public sealed class CategoryCreatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<CategoryCreatedEvent>
{
    public async ValueTask Handle(CategoryCreatedEvent e, CancellationToken cancellationToken)
    {
        CategoryModel category = new(e);

        await context.Categories.AddAsync(category, cancellationToken);
    }
}
