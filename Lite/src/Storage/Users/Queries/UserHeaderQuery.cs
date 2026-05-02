using Domain.Shared;
using Domain.UserAggregate.UserEntity;
using Mediator;

namespace Storage.Users.Queries;

public sealed record UserHeaderQuery(UserId User) : IQuery<ImageFile?> { }

public sealed class UserHeaderQueryHandler(IImageFileManager manager)
    : IQueryHandler<UserHeaderQuery, ImageFile?>
{
    public async ValueTask<ImageFile?> Handle(
        UserHeaderQuery request,
        CancellationToken cancellationToken
    )
    {
        manager.TryGet(request.User, out var file);

        return file;
    }
}
