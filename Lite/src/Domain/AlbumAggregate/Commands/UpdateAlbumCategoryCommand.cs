using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.Services;
using Domain.CategoryAggregate.CategoryEntity;
using Domain.Shared;
using Mediator;

namespace Domain.AlbumAggregate.Commands;

public sealed record class UpdateAlbumCategoryCommand(
    AlbumId Album,
    CategoryId Category,
    Actor Actor
) : ICommand { }

internal sealed class UpdateAlbumCategoryCommandHandler(
    IAlbumRepository repository,
    ICategoryExistenceChecker checker
) : ICommandHandler<UpdateAlbumCategoryCommand>
{
    public async ValueTask<Unit> Handle(
        UpdateAlbumCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var album = await repository.GetAsync(request.Album, cancellationToken);

        await album.UpdateCategory(request, checker);

        return Unit.Value;
    }
}
