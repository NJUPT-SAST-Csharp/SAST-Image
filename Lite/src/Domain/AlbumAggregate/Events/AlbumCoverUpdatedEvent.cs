using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.ImageEntity;
using Domain.Event;
using Domain.Shared;

namespace Domain.AlbumAggregate.Events;

public abstract record class AlbumCoverUpdatedEvent(AlbumId Album) : IDomainEvent;

public sealed record class AlbumCoverUpdatedManuallyEvent(AlbumId Album, ImageFile File)
    : AlbumCoverUpdatedEvent(Album);

public sealed record class AlbumCoverUpdatedAutomaticallyEvent(AlbumId Album, ImageId? Image)
    : AlbumCoverUpdatedEvent(Album);
