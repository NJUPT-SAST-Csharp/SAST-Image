using Domain.Event;
using Domain.File;
using Domain.Filters;
using Domain.User.Events;
using Orleans.Concurrency;

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

    public async ValueTask UpdateAvatar(
        Immutable<byte[]> file,
        CancellationToken cancellationToken = default
    )
    {
        var manager = GrainFactory.GetGrain<IFileManagerGrain>(Guid.Empty);
        var key = await manager.UploadAsync(file, cancellationToken);
        RaiseEvent(new AvatarUpdatedEvent(Id, key));
    }

    public async ValueTask UpdateHeader(
        Immutable<byte[]> file,
        CancellationToken cancellationToken = default
    )
    {
        var manager = GrainFactory.GetGrain<IFileManagerGrain>(Guid.Empty);
        var key = await manager.UploadAsync(file, cancellationToken);
        RaiseEvent(new HeaderUpdatedEvent(Id, key));
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

    public override void Apply(DomainEventBase e)
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
