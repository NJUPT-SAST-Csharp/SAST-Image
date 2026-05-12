using System.Text.Json.Serialization;
using Domain.Album.Events;
using Domain.Category.Events;
using Domain.User.Events;

namespace Domain.Event;

[Immutable]
[JsonPolymorphic]
// Category
[DerivedEvent<CategoryCreatedEvent>]
[DerivedEvent<CategoryDeletedEvent>]
[DerivedEvent<CategoryUpdatedEvent>]
// User
[DerivedEvent<ProfileUpdatedEvent>]
[DerivedEvent<AvatarUpdatedEvent>]
[DerivedEvent<HeaderUpdatedEvent>]
[DerivedEvent<UserRegisteredEvent>]
//Album
[DerivedEvent<AlbumCreatedEvent>]
[DerivedEvent<AlbumRemovedEvent>]
[DerivedEvent<AlbumUpdatedEvent>]
[DerivedEvent<AlbumImageAddedEvent>]
[DerivedEvent<AlbumImageUpdatedEvent>]
[DerivedEvent<AlbumImageRemovedEvent>]
public abstract record class DomainEventBase : Mediator.IRequest
{
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}

public sealed class DerivedEventAttribute<TEvent> : DerivedAttribute<TEvent, DomainEventBase>
    where TEvent : DomainEventBase;

public interface IDomainEventHandler<TDomainEvent> : Mediator.IRequestHandler<TDomainEvent>
    where TDomainEvent : DomainEventBase;
