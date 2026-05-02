using Domain.Event;
using Domain.UserAggregate.Events;
using Query.Database;

namespace Query.Users.EventHandlers;

public sealed class UserRegisteredEventHandler(QueryDbContext context)
    : IDomainEventHandler<UserRegisteredEvent>
{
    public async ValueTask Handle(UserRegisteredEvent e, CancellationToken cancellationToken)
    {
        UserModel user = new(e);

        await context.Users.AddAsync(user, cancellationToken);
    }
}
