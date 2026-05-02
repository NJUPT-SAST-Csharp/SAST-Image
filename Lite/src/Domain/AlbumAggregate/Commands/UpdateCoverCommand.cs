using Domain.AlbumAggregate.AlbumEntity;
using Domain.Shared;
using Mediator;

namespace Domain.AlbumAggregate.Commands;

/// <summary>
/// Represents a command to update the cover image of a specified album.
/// </summary>
/// <param name="Album">The identifier of the album whose cover image is to be updated.</param>
/// <param name="CoverImage">The new cover image to assign to the album, or null to apply the latest image. </param>
/// <param name="Actor">The actor performing the update operation.</param>
public sealed record class UpdateCoverCommand(AlbumId Album, ImageFile? CoverImage, Actor Actor)
    : ICommand { }

internal sealed class UpdateCoverCommandHandler(IAlbumRepository repository)
    : ICommandHandler<UpdateCoverCommand>
{
    public async ValueTask<Unit> Handle(
        UpdateCoverCommand request,
        CancellationToken cancellationToken
    )
    {
        var album = await repository.GetAsync(request.Album, cancellationToken);

        album.UpdateCover(request);

        return Unit.Value;
    }
}
