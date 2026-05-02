using Domain.Shared;
using Domain.UserAggregate.UserEntity;
using Mediator;

namespace Storage.Users.Queries;

public sealed record class UserAvatarQuery(UserId User) : IQuery<ImageFile?> { }

public sealed class UserAvatarQueryHandler(IImageFileManager manager)
    : IQueryHandler<UserAvatarQuery, ImageFile?>
{
    public async ValueTask<ImageFile?> Handle(
        UserAvatarQuery request,
        CancellationToken cancellationToken
    )
    {
        manager.TryGet(request.User, out var file);

        return file;
    }
}
