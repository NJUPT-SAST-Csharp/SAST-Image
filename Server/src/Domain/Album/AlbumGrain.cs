using System.Diagnostics.CodeAnalysis;
using Domain.Album.Events;
using Domain.Album.Image;
using Domain.Category;
using Domain.Event;
using Domain.Filters;

namespace Domain.Album;

internal sealed class AlbumGrain : DomainGrain<AlbumState>, IAlbumGrain
{
    public async ValueTask<AlbumId> Create(
        AlbumTitle title,
        AlbumDescription description,
        AlbumTags tags,
        CategoryId categoryId
    )
    {
        if (await GrainFactory.GetGrain<ICategoryGrain>(categoryId).Exists() is false)
            throw new CategoryNotFoundException(categoryId);

        RaiseEvent(new AlbumCreatedEvent(new(Id), title, description, tags, categoryId, Actor));

        return Id;
    }

    public ValueTask Remove()
    {
        if (Actor.Id != State.Author && Actor.IsAdmin is false)
            throw new ForbiddenException();

        RaiseEvent(new AlbumRemovedEvent(Id));

        return ValueTask.CompletedTask;
    }

    public async ValueTask Update(
        AlbumTitle? title,
        AlbumDescription? description,
        AlbumTags? tags,
        CategoryId? categoryId
    )
    {
        if (Actor.Id != State.Author && Actor.IsAdmin is false)
            throw new ForbiddenException();
        if (
            categoryId is { } id
            && await GrainFactory.GetGrain<ICategoryGrain>(id).Exists() is false
        )
            throw new CategoryNotFoundException(categoryId.Value);

        RaiseEvent(new AlbumUpdatedEvent(Id, title, description, tags, categoryId));
    }

    public ValueTask AddImage(ImageId id, ImageTitle title, ImageTags tags, ImageFile file)
    {
        if (Actor.Id != State.Author && Actor.IsAdmin is false)
            throw new ForbiddenException();
        if (State.Images.Contains(id))
            return ValueTask.CompletedTask;

        RaiseEvent(new AlbumImageAddedEvent(Id, id, title, tags, file, Actor));
        return ValueTask.CompletedTask;
    }

    public ValueTask UpdateImage(ImageId id, ImageTitle? title, ImageTags? tags)
    {
        if (Actor.Id != State.Author && !Actor.IsAdmin is false)
            throw new ForbiddenException();

        if (State.Images.Contains(id) is false)
            return ValueTask.CompletedTask;

        RaiseEvent(new AlbumImageUpdatedEvent(Id, id, title, tags));
        return ValueTask.CompletedTask;
    }

    public ValueTask RemoveImage(ImageId id)
    {
        if (Actor.Id != State.Author && Actor.IsAdmin is false)
            throw new ForbiddenException();

        var exists = State.Images.Contains(id);
        if (exists is false)
            return ValueTask.CompletedTask;

        RaiseEvent(new AlbumImageRemovedEvent(Id, id));
        return ValueTask.CompletedTask;
    }

    public ValueTask Subscribe()
    {
        if (State.Subscribes.Contains(Actor.Id))
            return ValueTask.CompletedTask;
        var subscribes = State.Subscribes.Append(Actor.Id);

        RaiseEvent(new AlbumUpdatedEvent(Id, Subscribes: subscribes));
        return ValueTask.CompletedTask;
    }

    public ValueTask Unsubscribe()
    {
        if (State.Subscribes.Contains(Actor.Id) is false)
            return ValueTask.CompletedTask;
        var subscribes = State.Subscribes.Filter(Actor.Id);

        RaiseEvent(new AlbumUpdatedEvent(Id, Subscribes: subscribes));
        return ValueTask.CompletedTask;
    }

    public ValueTask LikeImage(ImageId id)
    {
        if (State.Images.TryFind(id, out var image) is false)
            return ValueTask.CompletedTask;
        if (image.Likes.Contains(Actor.Id))
            return ValueTask.CompletedTask;

        var likes = image.Likes.Append(Actor.Id);

        RaiseEvent(new AlbumImageUpdatedEvent(Id, id, Likes: likes));
        return ValueTask.CompletedTask;
    }

    public ValueTask UnLikeImage(ImageId id)
    {
        if (State.Images.TryFind(id, out var image) is false)
            return ValueTask.CompletedTask;

        var likes = image.Likes.Filter(Actor.Id);

        RaiseEvent(new AlbumImageUpdatedEvent(Id, id, Likes: likes));
        return ValueTask.CompletedTask;
    }
}

internal sealed class AlbumState : DomainStateBase, IDomainEventApplyable
{
    public UserId Author { get; private set; }
    public UserId[] Subscribes { get; private set; } = [];
    public ImageState[] Images { get; private set; } = [];

    public void Apply(DomainEventBase e)
    {
        (Author, Images, Subscribes, RecordExists) = e switch
        {
            AlbumCreatedEvent a => (a.Actor.Id, Images, Subscribes, true),
            AlbumUpdatedEvent a => (Author, Images, a.Subscribes ?? Subscribes, RecordExists),
            AlbumRemovedEvent => (Author, Images, Subscribes, false),
            //
            AlbumImageUpdatedEvent i => (Author, Images.Update(i), Subscribes, RecordExists),
            AlbumImageAddedEvent i => (Author, Images.Append(i.ImageId), Subscribes, RecordExists),
            AlbumImageRemovedEvent i => (
                Author,
                Images.Filter(i.ImageId),
                Subscribes,
                RecordExists
            ),
            _ => throw new InvalidOperationException($"Unknown event type: {e.GetType().Name}"),
        };
    }
}

file static class CollectionExtensions
{
    extension<T>(IEnumerable<T> elements)
        where T : IEquatable<T>
    {
        public T[] Filter(T element) => [.. elements.Where(e => !e.Equals(element))];

        public T[] Append(T element) => [.. elements, element];
    }

    extension(ImageState[] images)
    {
        public ImageState[] Update(AlbumImageUpdatedEvent e)
        {
            if (e.Likes is null)
                return images;
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].Id == e.ImageId)
                {
                    images[i].Likes = e.Likes;
                    break;
                }
            }
            return images;
        }

        public ImageState[] Filter(ImageId id) => Array.FindAll(images, i => i.Id != id);

        public ImageState[] Append(ImageId id) => [.. images, new ImageState { Id = id }];

        public bool Contains(ImageId id) => Array.Exists(images, i => i.Id == id);

        public bool TryFind(ImageId id, [NotNullWhen(true)] out ImageState? item)
        {
            item = Array.Find(images, i => i.Id == id);
            return item != null;
        }
    }
}
