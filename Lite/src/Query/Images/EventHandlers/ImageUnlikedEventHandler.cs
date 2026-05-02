using Domain.AlbumAggregate.Events;
using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Images.EventHandlers;

public sealed class ImageUnlikedEventHandler(QueryDbContext context)
    : IDomainEventHandler<ImageUnlikedEvent>
{
    public async ValueTask Handle(ImageUnlikedEvent e, CancellationToken cancellationToken)
    {
        await context
            .Likes.Where(like => like.User == e.User.Value)
            .Where(like => like.Image == e.Image.Value)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
