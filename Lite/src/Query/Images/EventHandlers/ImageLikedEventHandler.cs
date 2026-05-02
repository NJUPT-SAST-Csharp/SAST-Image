using Domain.AlbumAggregate.Events;
using Domain.Event;
using Query.Database;

namespace Query.Images.EventHandlers;

public sealed class ImageLikedEventHandler(QueryDbContext context)
    : IDomainEventHandler<ImageLikedEvent>
{
    public async ValueTask Handle(ImageLikedEvent e, CancellationToken cancellationToken)
    {
        LikeModel like = new(e.Image.Value, e.User.Value);

        await context.Likes.AddAsync(like, cancellationToken);
    }
}
