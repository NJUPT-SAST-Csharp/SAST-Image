using Domain.Album.Image;
using Domain.Event;

namespace Domain.Album.Events;

public sealed record AlbumImageAddedEvent(
    AlbumId Id,
    ImageId ImageId,
    ImageTitle Title,
    ImageTags Tags,
    ImageFile File,
    Actor Uploader
) : DomainEventBase;
