using Domain.AlbumAggregate.ImageEntity;
using Domain.Shared;
using Mediator;

namespace Storage.Images.Queries;

public sealed record ImageFileQuery(ImageId Image, ImageKind Kind, Actor Actor)
    : IQuery<ImageFile?>;

public sealed class ImageFileQueryHandler(
    IImageFileManager manager,
    IImageAvailabilityChecker checker
) : IQueryHandler<ImageFileQuery, ImageFile?>
{
    public async ValueTask<ImageFile?> Handle(
        ImageFileQuery request,
        CancellationToken cancellationToken
    )
    {
        bool result = await checker.CheckAsync(request.Image, request.Actor, cancellationToken);

        if (result == false)
            return null;

        // NOTE: consistency problems.
        manager.TryGet(request.Image, out var file);
        return file;
    }
}
