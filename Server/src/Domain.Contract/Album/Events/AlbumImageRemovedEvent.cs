using Domain.Event;

namespace Domain.Album.Events;

public sealed record AlbumImageRemovedEvent(AlbumId Id, ImageId ImageId) : DomainEventBase;
