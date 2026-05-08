using Domain.Event;

namespace Domain.User.Events;

public sealed record class AvatarUpdatedEvent(UserId Id, ImageFile File) : DomainEventBase;
