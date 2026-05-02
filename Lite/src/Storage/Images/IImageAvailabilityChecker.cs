using Domain.AlbumAggregate.ImageEntity;
using Domain.Shared;

namespace Storage.Images;

public interface IImageAvailabilityChecker
{
    public Task<bool> CheckAsync(
        ImageId image,
        Actor actor,
        CancellationToken cancellationToken = default
    );
}
