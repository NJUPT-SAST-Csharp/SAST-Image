using Domain.Category.Events;
using Domain.Event;
using Query.Database;

namespace Query.Categories.EventHandlers;

public sealed class CategoryCreatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<CategoryCreatedEvent>
{
    public async ValueTask Handle(CategoryCreatedEvent e, CancellationToken cancellationToken)
    {
        CategoryModel category = new()
        {
            Id = e.Id,
            Name = e.Name.Value,
            Description = e.Description.Value,
        };

        await context.Categories.AddAsync(category, cancellationToken);
    }
}
