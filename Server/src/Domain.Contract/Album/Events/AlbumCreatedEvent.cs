using Domain.Event;

namespace Domain.Album.Events;

public sealed record class AlbumCreatedEvent(
    AlbumId Id,
    AlbumTitle Title,
    AlbumDescription Description,
    AlbumTags Tags,
    CategoryId CategoryId,
    Actor Actor
) : DomainEventBase;
