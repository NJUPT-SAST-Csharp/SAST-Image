using Domain.User;
using Mediator;
using Storage.Services;

namespace Storage.Users.Queries;

public sealed record UserHeaderQuery(UserId User) : IQuery<Stream?> { }

public sealed class UserHeaderQueryHandler(IImageFileManager manager)
    : IQueryHandler<UserHeaderQuery, Stream?>
{
    public async ValueTask<Stream?> Handle(
        UserHeaderQuery request,
        CancellationToken cancellationToken
    )
    {
        return manager.GetStream(request.User);
    }
}
