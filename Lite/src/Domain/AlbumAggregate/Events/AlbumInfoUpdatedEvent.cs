using Domain.AlbumAggregate.AlbumEntity;
using Domain.Event;

namespace Domain.AlbumAggregate.Events;

public sealed record class AlbumInfoUpdatedEvent(
    AlbumId Id,
    AlbumTitle? Title,
    AlbumDescription? Description,
    AlbumTags? Tags
) : IDomainEvent;
