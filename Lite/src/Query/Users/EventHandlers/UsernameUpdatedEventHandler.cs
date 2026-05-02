using Domain.Event;
using Domain.UserAggregate.Events;
using Query.Database;

namespace Query.Users.EventHandlers;

public sealed class UsernameUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<UsernameResetEvent>
{
    public async ValueTask Handle(UsernameResetEvent e, CancellationToken cancellationToken)
    {
        var user = await context.Users.GetAsync(u => u.Id == e.UserId.Value, cancellationToken);

        user.Username = e.Username.Value;
    }
}
