using Domain.Album.Image;
using Domain.Event;
using Domain.User;

namespace Domain.Album.Events;

public sealed record AlbumImageUpdatedEvent(
    AlbumId Id,
    ImageId ImageId,
    ImageTitle? Title = null,
    ImageTags? Tags = null,
    UserId[]? Likes = null
) : DomainEventBase;
