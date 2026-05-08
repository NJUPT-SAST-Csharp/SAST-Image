using Domain.Event;

namespace Domain.Album.Events;

public sealed record class AlbumRemovedEvent(long Id) : DomainEventBase;
