using Domain.Category.Events;
using Domain.Event;
using Mediator;
using Query.Database;

namespace Query.Category.EventHandlers;

public sealed class CategoryUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<CategoryUpdatedEvent>
{
    public async ValueTask<Unit> Handle(CategoryUpdatedEvent e, CancellationToken cancellationToken)
    {
        var category = await context.Categories.GetAsync(c => c.Id == e.Id, cancellationToken);

        if (e.Name is { Value: var name })
            category.Name = name;
        if (e.Description is { Value: var description })
            category.Description = description;

        return Unit.Value;
    }
}
