using Domain.Event;
using Domain.File;

namespace Domain.User.Events;

public sealed record class HeaderUpdatedEvent(UserId Id, ImageFileKey File) : DomainEventBase;
