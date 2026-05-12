using Domain.User;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Users.Queries;

public readonly record struct UsernameExistence(bool IsExist);

public sealed record class UsernameExistenceQuery(Username Username) : IQuery<UsernameExistence>
{
    internal static readonly Func<QueryDbContext, string, Task<bool>> Query = EF.CompileAsyncQuery(
        (QueryDbContext context, string username) =>
            context
                .Users.AsNoTracking()
                .Where(user => EF.Functions.ILike(user.Username, username))
                .Any()
    );
}

public sealed class UsernameExistenceQueryHandler(QueryDbContext context)
    : IQueryHandler<UsernameExistenceQuery, UsernameExistence>
{
    public async ValueTask<UsernameExistence> Handle(
        UsernameExistenceQuery request,
        CancellationToken cancellationToken
    )
    {
        bool exist = await UsernameExistenceQuery
            .Query(context, request.Username.Value)
            .WaitAsync(cancellationToken);

        return new UsernameExistence(exist);
    }
}
