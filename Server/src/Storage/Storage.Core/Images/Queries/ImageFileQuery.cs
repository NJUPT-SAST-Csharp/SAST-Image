using Domain;
using Domain.Album.Image;
using Mediator;
using Storage.Services;

namespace Storage.Images.Queries;

public sealed record ImageFileQuery(ImageId Image, ImageKind Kind, Actor Actor) : IQuery<Stream?>;

public sealed class ImageFileQueryHandler(IImageFileManager manager, IAccessChecker checker)
    : IQueryHandler<ImageFileQuery, Stream?>
{
    public async ValueTask<Stream?> Handle(
        ImageFileQuery request,
        CancellationToken cancellationToken
    )
    {
        bool result = await checker.HasAccessAsync(request.Actor, request.Image, cancellationToken);
        if (result is false)
            return null;

        return manager.GetStream(request.Image);
    }
}
