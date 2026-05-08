using Domain.Album.Image;
using Domain.Category;
using Domain.Filters;

namespace Domain.Album;

[Alias("album_grain")]
public interface IAlbumGrain : IGrainWithIntegerKey
{
    [AccessControl]
    [EnsureUniqueId]
    [AllowRecordNotExists]
    [Alias("album_create")]
    public ValueTask<AlbumId> Create(
        AlbumTitle title,
        AlbumDescription description,
        AlbumTags tags,
        CategoryId categoryId
    );

    [AccessControl]
    [Alias("album_update")]
    public ValueTask Update(
        AlbumTitle? title,
        AlbumDescription? description,
        AlbumTags? tags,
        CategoryId? categoryId
    );

    [AccessControl]
    [Alias("album_remove")]
    public ValueTask Remove();

    [AccessControl]
    [Alias("album_subscribe")]
    public ValueTask Subscribe();

    [AccessControl]
    [Alias("album_unsubscribe")]
    public ValueTask Unsubscribe();

    [AccessControl]
    [Alias("album_add_image")]
    public ValueTask AddImage(ImageId id, ImageTitle title, ImageTags tags, ImageFile file);

    [AccessControl]
    [Alias("album_update_image")]
    public ValueTask UpdateImage(ImageId id, ImageTitle? title, ImageTags? tags);

    [AccessControl]
    [Alias("album_remove_image")]
    public ValueTask RemoveImage(ImageId id);

    [AccessControl]
    [Alias("album_like_image")]
    public ValueTask LikeImage(ImageId id);

    [AccessControl]
    [Alias("album_unlike_image")]
    public ValueTask UnLikeImage(ImageId id);
}
