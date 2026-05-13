using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.User;

public sealed class UsernameExistenceQueryHandler(QueryDbContext context)
    : IQueryHandler<UsernameExistenceQuery, UsernameExistence>
{
    private static readonly Func<QueryDbContext, string, Task<bool>> Query = EF.CompileAsyncQuery(
        (QueryDbContext context, string username) =>
            context
                .Users.AsNoTracking()
                .Where(user => EF.Functions.ILike(user.Username, username))
                .Any()
    );

    public async ValueTask<UsernameExistence> Handle(
        UsernameExistenceQuery request,
        CancellationToken cancellationToken
    )
    {
        bool exists = await Query(context, request.Username.Value).WaitAsync(cancellationToken);

        return new UsernameExistence(exists);
    }
}
