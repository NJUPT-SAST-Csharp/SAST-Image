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
