using Domain.AlbumAggregate.ImageEntity;
using Domain.Shared;

namespace Query.Images;

public interface IImageAvailabilityChecker
{
    public Task<bool> CheckAsync(
        ImageId id,
        Actor actor,
        CancellationToken cancellationToken = default
    );
}
