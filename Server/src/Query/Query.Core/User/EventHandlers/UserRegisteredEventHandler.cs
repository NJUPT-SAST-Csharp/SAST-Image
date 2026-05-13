using Domain.Event;
using Domain.User.Events;
using Mediator;
using Query.Database;

namespace Query.User.EventHandlers;

public sealed class UserRegisteredEventHandler(QueryDbContext context)
    : IDomainEventHandler<UserRegisteredEvent>
{
    public async ValueTask<Unit> Handle(UserRegisteredEvent e, CancellationToken cancellationToken)
    {
        UserModel user = new(e);

        await context.Users.AddAsync(user, cancellationToken);

        return Unit.Value;
    }
}
