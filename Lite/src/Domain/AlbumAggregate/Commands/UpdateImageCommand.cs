using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.ImageEntity;
using Domain.Shared;
using Mediator;

namespace Domain.AlbumAggregate.Commands;

public sealed record class UpdateImageCommand(
    AlbumId AlbumId,
    ImageId ImageId,
    ImageTitle? Title,
    ImageTags? Tags,
    Actor Actor
) : ICommand;

internal sealed class UpdateImageCommandHandler(IAlbumRepository repository)
    : ICommandHandler<UpdateImageCommand>
{
    public async ValueTask<Unit> Handle(
        UpdateImageCommand command,
        CancellationToken cancellationToken
    )
    {
        var album = await repository.GetAsync(command.AlbumId, cancellationToken);

        album.UpdateImage(command);

        return Unit.Value;
    }
}
