using Domain.AlbumAggregate.AlbumEntity;
using Domain.Shared;
using Mediator;

namespace Domain.AlbumAggregate.Commands;

public sealed record class UpdateAlbumInfoCommand(
    AlbumId Id,
    AlbumTitle? Title,
    AlbumDescription? Description,
    AlbumTags? Tags,
    Actor Actor
) : ICommand;

internal sealed class UpdateAlbumInfoCommandHandler(IAlbumRepository repository)
    : ICommandHandler<UpdateAlbumInfoCommand>
{
    public async ValueTask<Unit> Handle(
        UpdateAlbumInfoCommand request,
        CancellationToken cancellationToken
    )
    {
        var album = await repository.GetAsync(request.Id, cancellationToken);
        album.UpdateInfo(request);
        return Unit.Value;
    }
}
