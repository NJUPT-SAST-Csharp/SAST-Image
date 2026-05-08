using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.Users.Queries;

public sealed record class UserProfileDto(
    long Id,
    string Username,
    string Nickname,
    string Biography
);

public sealed record UserProfileQuery(UserId User) : IQuery<UserProfileDto?>
{
    internal static readonly Func<QueryDbContext, long, Task<UserProfileDto?>> Query =
        EF.CompileAsyncQuery(
            (QueryDbContext context, long userId) =>
                context
                    .Users.AsNoTracking()
                    .Where(user => user.Id == userId)
                    .Select(user => new UserProfileDto(
                        user.Id,
                        user.Username,
                        user.Nickname,
                        user.Biography
                    ))
                    .FirstOrDefault()
        );
}

public sealed class UserProfileQueryHandler(QueryDbContext context)
    : IQueryHandler<UserProfileQuery, UserProfileDto?>
{
    public async ValueTask<UserProfileDto?> Handle(
        UserProfileQuery request,
        CancellationToken cancellationToken
    )
    {
        return await UserProfileQuery.Query(context, request.User).WaitAsync(cancellationToken);
    }
}
