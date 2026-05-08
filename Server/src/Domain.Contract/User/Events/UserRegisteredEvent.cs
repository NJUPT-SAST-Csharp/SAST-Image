using Domain.Event;

namespace Domain.User.Events;

public sealed record class UserRegisteredEvent(
    UserId Id,
    Username Username,
    Nickname Nickname,
    Biography Biography
) : DomainEventBase;
