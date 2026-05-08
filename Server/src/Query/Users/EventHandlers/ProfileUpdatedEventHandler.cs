using Domain.Event;
using Domain.User.Events;
using Query.Database;

namespace Query.Users.EventHandlers;

public sealed class ProfileUpdatedEventHandler(QueryDbContext context)
    : IDomainEventHandler<ProfileUpdatedEvent>
{
    public async ValueTask Handle(ProfileUpdatedEvent e, CancellationToken cancellationToken)
    {
        var user = await context.Users.GetAsync(u => u.Id == e.Id, cancellationToken);

        if (e.Biography is { Value: var biography })
            user.Biography = biography;
        if (e.Nickname is { Value: var nickname })
            user.Nickname = nickname;
        if (e.Username is { Value: var username })
            user.Username = username;
    }
}
