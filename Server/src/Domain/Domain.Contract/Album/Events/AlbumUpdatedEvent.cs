using Domain.Category;
using Domain.Event;
using Domain.User;

namespace Domain.Album.Events;

public sealed record AlbumUpdatedEvent(
    long Id,
    AlbumTitle? Title = null,
    AlbumDescription? Description = null,
    AlbumTags? Tags = null,
    CategoryId? CategoryId = null,
    UserId[]? Subscribes = null
) : DomainEventBase;
