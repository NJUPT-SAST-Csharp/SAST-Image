using Domain.Event;

namespace Domain.User.Events;

public sealed record class HeaderUpdatedEvent(UserId Id, ImageFile File) : DomainEventBase;
