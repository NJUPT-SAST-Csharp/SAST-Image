using Mediator;
using Microsoft.EntityFrameworkCore;
using Query.Database;

namespace Query.User;

public sealed class UserProfileQueryHandler(QueryDbContext context)
    : IQueryHandler<UserProfileQuery, UserProfileDto?>
{
    private static readonly Func<QueryDbContext, long, Task<UserProfileDto?>> Query =
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

    public async ValueTask<UserProfileDto?> Handle(
        UserProfileQuery request,
        CancellationToken cancellationToken
    )
    {
        return await Query(context, request.User.Value).WaitAsync(cancellationToken);
    }
}

public sealed class UserByUsernameQueryHandler(QueryDbContext context)
    : IQueryHandler<UserByUsernameQuery, UserProfileDto?>
{
    private static readonly Func<QueryDbContext, string, Task<UserProfileDto?>> Query =
        EF.CompileAsyncQuery(
            (QueryDbContext context, string username) =>
                context
                    .Users.AsNoTracking()
                    .Where(user => EF.Functions.ILike(user.Username, username))
                    .Select(user => new UserProfileDto(
                        user.Id,
                        user.Username,
                        user.Nickname,
                        user.Biography
                    ))
                    .FirstOrDefault()
        );

    public async ValueTask<UserProfileDto?> Handle(
        UserByUsernameQuery request,
        CancellationToken cancellationToken
    )
    {
        return await Query(context, request.Username.Value).WaitAsync(cancellationToken);
    }
}
