using Domain.CategoryAggregate.Commands;
using Domain.CategoryAggregate.Events;
using Domain.CategoryAggregate.Services;
using Domain.Entity;
using Domain.Shared;

namespace Domain.CategoryAggregate.CategoryEntity;

public sealed class Category : EntityBase<CategoryId>
{
    [Obsolete("For ORM", true)]
    private Category()
        : base(default) { }

    private Category(CreateCategoryCommand command)
        : base(CategoryId.GenerateNew())
    {
        _name = command.Name;
    }

    internal static async Task<CategoryId> CreateAsync(
        CreateCategoryCommand command,
        ICategoryNameUniquenessChecker checker,
        ICategoryRepository repository,
        CancellationToken cancellationToken
    )
    {
        await checker.CheckAsync(command.Name, cancellationToken);

        var newOne = new Category(command);

        await repository.AddAsync(newOne, cancellationToken);

        newOne.AddDomainEvent(
            new CategoryCreatedEvent(newOne.Id, command.Name, command.Description)
        );

        return newOne.Id;
    }

    private CategoryName _name;

    public async Task Update(
        UpdateCategoryCommand command,
        ICategoryNameUniquenessChecker checker,
        CancellationToken cancellationToken
    )
    {
        if (command.Actor.IsAdmin == false)
            throw new NoPermissionException();
        if (command.Description is null && (command.Name is not { } n || n == _name))
            return;
        if (command.Name is { } name)
            await checker.CheckAsync(name, cancellationToken);

        AddDomainEvent(new CategoryUpdatedEvent(Id, command.Name, command.Description));
    }
}
