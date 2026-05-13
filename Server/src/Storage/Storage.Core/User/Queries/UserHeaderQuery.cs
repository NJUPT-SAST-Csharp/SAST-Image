using Domain.User;
using Mediator;
using Storage.Services;

namespace Storage.User.Queries;

public sealed record UserHeaderQuery(UserId User) : IQuery<Stream?> { }

public sealed class UserHeaderQueryHandler(LocalImageFileManager manager)
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
