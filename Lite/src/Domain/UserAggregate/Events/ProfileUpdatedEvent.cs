using Domain.Event;
using Domain.UserAggregate.UserEntity;

namespace Domain.UserAggregate.Events;

public sealed record class ProfileUpdatedEvent(UserId Id, Nickname? Nickname, Biography? Biography)
    : IDomainEvent;
