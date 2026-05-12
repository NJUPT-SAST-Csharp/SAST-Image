using Domain.User;
using Mediator;
using Storage.Services;

namespace Storage.Users.Queries;

public sealed record class UserAvatarQuery(UserId User) : IQuery<Stream?> { }

public sealed class UserAvatarQueryHandler(IImageFileManager manager)
    : IQueryHandler<UserAvatarQuery, Stream?>
{
    public async ValueTask<Stream?> Handle(
        UserAvatarQuery request,
        CancellationToken cancellationToken
    )
    {
        return manager.GetStream(request.User);
    }
}
