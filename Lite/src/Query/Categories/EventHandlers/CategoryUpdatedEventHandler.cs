using Domain.CategoryAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Categories.EventHandlers;

public sealed class CategoryUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<CategoryUpdatedEvent>
{
    public async ValueTask Handle(CategoryUpdatedEvent e, CancellationToken cancellationToken)
    {
        var category = await context.Categories.GetAsync(
            c => c.Id == e.Id.Value,
            cancellationToken
        );

        if (e.Name is { Value: var name })
            category.Name = name;
        if (e.Description is { Value: var description })
            category.Description = description;
    }
}
