using System.Runtime.CompilerServices;
using Domain.AlbumAggregate.AlbumEntity;
using Query.Images;

namespace Query.Albums;

internal static class AlbumModelExtensions
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    private static extern AlbumModel _Create();

    extension(AlbumModel)
    {
        public static AlbumModel Create(
            long id,
            string title,
            string description,
            long authorId,
            long categoryId,
            AccessLevelValue accessLevel,
            string[] tags,
            DateTime createdAt,
            DateTime updatedAt,
            DateTime? removedAt,
            List<SubscribeModel> subscribes,
            List<ImageModel> images
        )
        {
            var album = _Create();

            album.Set(a => a.Id, id);
            album.Set(a => a.Title, title);
            album.Set(a => a.Description, description);
            album.Set(a => a.AuthorId, authorId);
            album.Set(a => a.CategoryId, categoryId);
            album.Set(a => a.AccessLevel, accessLevel);
            album.Set(a => a.Tags, tags);
            album.Set(a => a.CreatedAt, createdAt);
            album.Set(a => a.UpdatedAt, updatedAt);
            album.Set(a => a.RemovedAt, removedAt);
            album.Set(a => a.Subscribes, subscribes);
            album.Set(a => a.Images, images);

            return album;
        }
    }
}
