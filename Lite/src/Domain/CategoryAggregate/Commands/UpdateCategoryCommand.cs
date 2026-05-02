using Domain.CategoryAggregate.CategoryEntity;
using Domain.CategoryAggregate.Services;
using Domain.Shared;
using Mediator;

namespace Domain.CategoryAggregate.Commands;

public sealed record class UpdateCategoryCommand(
    CategoryId Id,
    CategoryName? Name,
    CategoryDescription? Description,
    Actor Actor
) : ICommand;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository repository,
    ICategoryNameUniquenessChecker checker
) : ICommandHandler<UpdateCategoryCommand>
{
    public async ValueTask<Unit> Handle(
        UpdateCategoryCommand command,
        CancellationToken cancellationToken
    )
    {
        var category = await repository.GetAsync(command.Id, cancellationToken);

        await category.Update(command, checker, cancellationToken);

        return Unit.Value;
    }
}
