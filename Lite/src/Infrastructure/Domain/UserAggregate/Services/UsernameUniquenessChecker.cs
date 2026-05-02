using Domain.Database;
using Domain.UserAggregate.Exceptions;
using Domain.UserAggregate.UserEntity;
using Microsoft.EntityFrameworkCore;

namespace Domain.UserAggregate.Services;

internal sealed class UsernameUniquenessChecker(DomainDbContext context)
    : IUsernameUniquenessChecker
{
    public async Task CheckAsync(Username username, CancellationToken cancellationToken = default)
    {
        bool isExisting = await context
            .Users.FromSql($"Select 1 From domain.users WHERE username ILIKE {username.Value}")
            .AsNoTracking()
            .AnyAsync(cancellationToken);

        if (isExisting)
            throw new UsernameDuplicateException(username);
    }
}
