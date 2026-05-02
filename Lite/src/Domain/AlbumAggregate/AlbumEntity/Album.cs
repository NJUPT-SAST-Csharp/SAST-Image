using Domain.AlbumAggregate.Commands;
using Domain.AlbumAggregate.Events;
using Domain.AlbumAggregate.Exceptions;
using Domain.AlbumAggregate.ImageEntity;
using Domain.AlbumAggregate.Services;
using Domain.Entity;
using Domain.Shared;
using Domain.UserAggregate.UserEntity;

namespace Domain.AlbumAggregate.AlbumEntity;

public sealed class Album : EntityBase<AlbumId>
{
    [Obsolete("For ORM", true)]
    private Album()
        : base(default) { }

    private bool _removed = false;

    private bool _customCover = false;

    private AccessLevel _accessLevel;

    private readonly UserId _author;

    private readonly List<Subscribe> _subscribes = [];

    private readonly List<Image> _images = [];

    internal Album(CreateAlbumCommand command)
        : base(AlbumId.GenerateNew())
    {
        _author = command.Actor.Id;
    }

    internal static async Task<AlbumId> CreateAsync(
        CreateAlbumCommand command,
        ICategoryExistenceChecker categoryChecker,
        IAlbumRepository repository,
        CancellationToken cancellationToken
    )
    {
        await categoryChecker.CheckAsync(command.CategoryId, cancellationToken);

        Album album = new(command);

        await repository.AddAsync(album, cancellationToken);

        album.AddDomainEvent(
            new AlbumCreatedEvent(
                album.Id,
                command.Actor.Id,
                command.CategoryId,
                command.Title,
                command.Description,
                command.AccessLevel
            )
        );

        return album.Id;
    }

    public void UpdateAccessLevel(UpdateAccessLevelCommand command)
    {
        if (CanNotManage(command.Actor))
            throw new NoPermissionException();
        if (_removed)
            throw new AlbumRemovedException();
        if (_accessLevel == command.AccessLevel)
            return;

        _accessLevel = command.AccessLevel;

        AddDomainEvent(new AlbumAccessLevelUpdatedEvent(Id, command.AccessLevel));
    }

    public async Task UpdateCategory(
        UpdateAlbumCategoryCommand command,
        ICategoryExistenceChecker checker
    )
    {
        if (CanNotManage(command.Actor))
            throw new NoPermissionException();
        if (_removed)
            throw new AlbumRemovedException();

        await checker.CheckAsync(command.Category);

        AddDomainEvent(new AlbumCategoryUpdatedEvent(Id, command.Category));
    }

    internal void UpdateInfo(UpdateAlbumInfoCommand command)
    {
        if (CanNotManage(command.Actor))
            throw new NoPermissionException();
        if (_removed)
            throw new AlbumRemovedException();
        if (command.Title is null && command.Description is null && command.Tags is null)
            return;

        AddDomainEvent(
            new AlbumInfoUpdatedEvent(Id, command.Title, command.Description, command.Tags)
        );
    }

    public void UpdateCover(UpdateCoverCommand command)
    {
        if (CanNotManage(command.Actor))
            throw new NoPermissionException();
        if (_removed)
            throw new AlbumRemovedException();

        if (command.CoverImage is null)
        {
            _customCover = false;
            var imageId = _images.LatestAvailableImage?.Id;

            AddDomainEvent(new AlbumCoverUpdatedAutomaticallyEvent(Id, imageId));

            return;
        }

        _customCover = true;
        AddDomainEvent(new AlbumCoverUpdatedManuallyEvent(Id, command.CoverImage.Value));
    }

    public void Remove(RemoveAlbumCommand command)
    {
        if (CanNotManage(command.Actor))
            throw new NoPermissionException();
        if (_removed)
            return;

        _removed = true;

        AddDomainEvent(new AlbumRemovedEvent(Id));
        foreach (var image in _images)
        {
            image.AlbumRemoved(command);
        }
    }

    public void Restore(RestoreAlbumCommand command)
    {
        if (CanNotManage(command.Actor))
            throw new NoPermissionException();
        if (_removed == false)
            return;

        _removed = false;

        AddDomainEvent(new AlbumRestoredEvent(Id));
        foreach (var image in _images)
        {
            image.AlbumRestored(command);
        }
    }

    public void Subscribe(SubscribeCommand command)
    {
        if (_removed)
            throw new AlbumRemovedException();
        if (_subscribes.ContainsUser(command.Actor.Id))
            return;
        if (_accessLevel == AccessLevel.Private && CanNotManage(command.Actor))
            throw new NoPermissionException();

        _subscribes.Add(new(Id, command.Actor.Id));

        AddDomainEvent(new AlbumSubscribedEvent(Id, command.Actor.Id));
    }

    public void Unsubscribe(UnsubscribeCommand command)
    {
        if (_removed)
            throw new AlbumRemovedException();
        if (_subscribes.NotContainsUser(command.Actor.Id))
            return;
        if (_accessLevel == AccessLevel.Private && CanNotManage(command.Actor))
            throw new NoPermissionException();

        _subscribes.RemoveUser(command.Actor.Id);

        AddDomainEvent(new AlbumUnsubscribedEvent(Id, command.Actor.Id));
    }

    #region Image
    public ImageId AddImage(AddImageCommand command)
    {
        if (CanNotManageImages(command.Actor) && _accessLevel.OthersCanNotWrite)
            throw new NoPermissionException();
        if (_removed)
            throw new AlbumRemovedException();

        Image image = new(command);

        _images.Add(image);

        AddDomainEvent(
            new ImageAddedEvent(
                Id,
                image.Id,
                _author,
                command.Title,
                command.Tags,
                _accessLevel,
                command.File,
                DateTime.UtcNow,
                command.Actor.Id
            )
        );

        if (_customCover)
            return image.Id;

        AddDomainEvent(new AlbumCoverUpdatedAutomaticallyEvent(Id, image.Id));
        return image.Id;
    }

    public void RemoveImage(RemoveImageCommand command)
    {
        var image = _images.FindById(command.Image);

        if (
            CanNotManageImages(command.Actor)
            && (_accessLevel.OthersCanNotWrite || image.IsNotUploader(command.Actor))
        )
            throw new NoPermissionException();
        if (_removed)
            throw new AlbumRemovedException();
        if (image.IsRemoved)
            return;

        bool isLatestImage = image.Equals(_images.LatestAvailableImage);

        image.Remove(command);

        if (_customCover)
            return;

        if (isLatestImage is false)
            return;

        var imageId = _images.LatestAvailableImage?.Id;

        AddDomainEvent(new AlbumCoverUpdatedAutomaticallyEvent(Id, imageId));
    }

    public void RestoreImage(RestoreImageCommand command)
    {
        if (CanNotManageImages(command.Actor))
            throw new NoPermissionException();
        if (_removed)
            throw new AlbumRemovedException();

        var image = _images.FindById(command.Image);
        if (image.IsRemoved is false)
            return;

        image.Restore(command);

        if (image.Equals(_images.LatestAvailableImage))
        {
            AddDomainEvent(new AlbumCoverUpdatedAutomaticallyEvent(Id, image.Id));
        }
    }

    public void DeleteImage(DeleteImageCommand command)
    {
        if (CanNotManage(command.Actor))
            throw new NoPermissionException();

        bool isLatestImage = _images.LatestAvailableImage?.Id == command.Image;

        _images.DeleteImage(command.Image);

        if (_customCover)
            return;
        if (isLatestImage is false)
            return;

        var imageId = _images.LatestAvailableImage?.Id;

        AddDomainEvent(new AlbumCoverUpdatedAutomaticallyEvent(Id, imageId));
    }

    public void LikeImage(LikeImageCommand command)
    {
        if (_removed)
            throw new AlbumRemovedException();
        if (_accessLevel == AccessLevel.Private && CanNotManage(command.Actor))
            throw new NoPermissionException();

        var image = _images.FindById(command.Image);

        image.Like(command);
    }

    public void UnlikeImage(UnlikeImageCommand command)
    {
        if (_removed)
            throw new AlbumRemovedException();
        if (_accessLevel == AccessLevel.Private && CanNotManage(command.Actor))
            throw new NoPermissionException();

        var image = _images.FindById(command.Image);

        image.Unlike(command);
    }

    public void UpdateImage(UpdateImageCommand command)
    {
        if (_removed)
            throw new AlbumRemovedException();
        if (_accessLevel == AccessLevel.Private && CanNotManage(command.Actor))
            throw new NoPermissionException();
        if (command.Title is null && command.Tags is null)
            return;

        var image = _images.FindById(command.ImageId);

        image.Update(command);
    }

    #endregion

    #region Helper
    private bool IsOwnedBy(Actor actor) => _author == actor.Id;

    private bool CanManage(Actor actor) => IsOwnedBy(actor) || actor.IsAdmin;

    private bool CanNotManage(Actor actor) => !CanManage(actor);

    private bool CanManageImages(Actor actor) => CanManage(actor);

    private bool CanNotManageImages(Actor actor) => !CanManageImages(actor);

    #endregion
}
