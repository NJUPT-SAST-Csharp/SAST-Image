using Domain.Category.Events;
using Domain.Event;

namespace Domain.Category;

internal sealed class CategoryGrain() : DomainGrain<CategoryState>, ICategoryGrain
{
    public async ValueTask<CategoryId> Create(CategoryName name, CategoryDescription description)
    {
        RaiseEvent(new CategoryCreatedEvent(Id, name, description));

        return Id;
    }

    public ValueTask Delete()
    {
        RaiseEvent(new CategoryDeletedEvent(Id));

        DeactivateOnIdle();

        return ValueTask.CompletedTask;
    }

    public ValueTask Update(CategoryName? name, CategoryDescription? description)
    {
        RaiseEvent(new CategoryUpdatedEvent(Id, name, description));

        return ValueTask.CompletedTask;
    }

    public ValueTask<bool> Exists()
    {
        var exists = State.RecordExists;
        if (exists is false)
            DeactivateOnIdle();
        return ValueTask.FromResult(exists);
    }
}

internal sealed class CategoryState : DomainStateBase, IDomainEventApplyable
{
    public CategoryName Name { get; private set; }

    public void Apply(DomainEventBase e)
    {
        (Name, RecordExists) = e switch
        {
            CategoryCreatedEvent c => (c.Name, true),
            CategoryUpdatedEvent c => (c.Name ?? Name, RecordExists),
            CategoryDeletedEvent => (Name, false),
            _ => throw new InvalidOperationException($"Unknown event type: {e.GetType().Name}"),
        };
    }
}
