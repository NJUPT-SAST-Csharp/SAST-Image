using Domain;
using Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Storage.Database;

namespace Storage.Services;

public interface IAccessChecker
{
    ValueTask<bool> HasAccessAsync<TId>(
        Actor user,
        TId id,
        CancellationToken cancellationToken = default
    )
        where TId : ITypedId<TId>;
}

internal sealed class AccessChecker(StorageDbContext context) : IAccessChecker
{
    public async ValueTask<bool> HasAccessAsync<TId>(
        Actor user,
        TId id,
        CancellationToken cancellationToken = default
    )
        where TId : ITypedId<TId>
    {
        var hasAccess = await context
            .AccessControlList.AsNoTracking()
            .Where(a => a.ResourceId == id.Value)
            .Where(a =>
                user.IsAdmin
                || a.Level == AccessControlLevel.Public
                || (a.Level == AccessControlLevel.Auth && user.IsAuthenticated)
                || a.Users.Contains(user.Id)
            )
            .AnyAsync(cancellationToken);

        return hasAccess;
    }
}
