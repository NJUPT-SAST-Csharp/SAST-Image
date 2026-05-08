using Domain.Event;
using Domain.Filters;
using Domain.User.Events;

namespace Domain.User;

internal sealed class UserGrain(IUsernameUniquenessChecker usernameChecker)
    : DomainGrain<UserState>,
        IUserGrain,
        IIncomingGrainCallFilter
{
    public Task Invoke(IIncomingGrainCallContext context)
    {
        if (context.MethodName == nameof(Register))
            return context.Invoke();
        if (Id != Actor.Id)
            throw new ForbiddenException();
        return context.Invoke();
    }

    public async ValueTask<UserId> Register(
        Username username,
        Nickname nickname,
        Biography biography
    )
    {
        if (await usernameChecker.ExistsAsync(username))
            throw new UsernameAlreadyExistsException(username);

        RaiseEvent(new UserRegisteredEvent(Id, username, nickname, biography));
        return Id;
    }

    public ValueTask UpdateAvatar(ImageFile file)
    {
        RaiseEvent(new AvatarUpdatedEvent(Id, file));
        return ValueTask.CompletedTask;
    }

    public ValueTask UpdateHeader(ImageFile file)
    {
        RaiseEvent(new HeaderUpdatedEvent(Id, file));
        return ValueTask.CompletedTask;
    }

    public async ValueTask UpdateProfile(
        Username? username,
        Nickname? nickname,
        Biography? biography
    )
    {
        if (username is { } value)
            if (await usernameChecker.ExistsAsync(value))
                throw new UsernameAlreadyExistsException(value);

        RaiseEvent(new ProfileUpdatedEvent(Id, username, nickname, biography));
    }
}

internal sealed class UserState : DomainStateBase, IDomainEventApplyable
{
    public Username Username { get; private set; }

    public void Apply(DomainEventBase e)
    {
        (Username, RecordExists) = e switch
        {
            UserRegisteredEvent u => (u.Username, true),
            ProfileUpdatedEvent u => (u.Username ?? Username, RecordExists),
            AvatarUpdatedEvent or HeaderUpdatedEvent => (Username, RecordExists),
            _ => throw new InvalidOperationException($"Unknown event type: {e.GetType().Name}"),
        };
    }
}
