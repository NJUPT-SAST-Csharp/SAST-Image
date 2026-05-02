using System.Runtime.CompilerServices;
using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.ImageEntity;

namespace Query.Images;

internal static class ImageModelExtensions
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    private static extern ImageModel _Create();

    extension(ImageModel)
    {
        public static ImageModel Create(
            long id,
            string title,
            long albumId,
            long authorId,
            long uploaderId,
            string[] tags,
            DateTime uploadedAt,
            AccessLevelValue accessLevel,
            ImageStatusValue status,
            DateTime? removedAt,
            List<LikeModel> likes
        )
        {
            var Image = _Create();

            Image.Set(a => a.Id, id);
            Image.Set(a => a.Title, title);
            Image.Set(a => a.AlbumId, albumId);
            Image.Set(a => a.AuthorId, authorId);
            Image.Set(a => a.UploaderId, uploaderId);
            Image.Set(a => a.Tags, tags);
            Image.Set(a => a.UploadedAt, uploadedAt);
            Image.Set(a => a.AccessLevel, accessLevel);
            Image.Set(a => a.Status, status);
            Image.Set(a => a.RemovedAt, removedAt);
            Image.Set(a => a.Likes, likes);

            return Image;
        }
    }
}
