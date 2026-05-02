using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.ImageEntity;

namespace Query.Images;

internal sealed class ImageSeedData
{
    public static IEnumerable<ImageModel> Seed =>
        [
            ImageModel.Create(
                id: IDs.Images.GeneralImage,
                title: "Image 1",
                albumId: IDs.Albums.GeneralAlbum,
                authorId: IDs.Users.Admin,
                uploaderId: IDs.Users.Admin,
                tags: ["tag1", "tag2"],
                uploadedAt: DateTime.UtcNow.AddDays(-9),
                accessLevel: AccessLevelValue.PublicReadOnly,
                status: ImageStatusValue.Available,
                removedAt: null,
                likes: [new LikeModel(IDs.Images.GeneralImage, IDs.Users.Member)]
            ),
            ImageModel.Create(
                id: IDs.Images.MountainImage,
                title: "Image 2",
                albumId: IDs.Albums.GeneralAlbum,
                authorId: IDs.Users.Member,
                uploaderId: IDs.Users.Member,
                tags: ["nature", "mountain"],
                uploadedAt: DateTime.UtcNow.AddDays(-7),
                accessLevel: AccessLevelValue.PublicReadOnly,
                status: ImageStatusValue.Available,
                removedAt: null,
                likes:
                [
                    new LikeModel(IDs.Images.MountainImage, IDs.Users.Admin),
                    new LikeModel(IDs.Images.MountainImage, IDs.Users.Photographer),
                ]
            ),
            ImageModel.Create(
                id: IDs.Images.UrbanImage,
                title: "Image 3",
                albumId: IDs.Albums.CityNights,
                authorId: IDs.Users.Photographer,
                uploaderId: IDs.Users.Photographer,
                tags: ["urban", "night"],
                uploadedAt: DateTime.UtcNow.AddDays(-4),
                accessLevel: AccessLevelValue.Private,
                status: ImageStatusValue.Available,
                removedAt: null,
                likes: [new LikeModel(IDs.Images.UrbanImage, IDs.Users.Member)]
            ),
            ImageModel.Create(
                id: IDs.Images.PortraitImage,
                title: "Image 4",
                albumId: IDs.Albums.CityNights,
                authorId: IDs.Users.Admin,
                uploaderId: IDs.Users.Curator,
                tags: ["portrait", "studio"],
                uploadedAt: DateTime.UtcNow.AddDays(-3),
                accessLevel: AccessLevelValue.PublicReadOnly,
                status: ImageStatusValue.Removed,
                removedAt: DateTime.UtcNow.AddDays(-1),
                likes: []
            ),
            ImageModel.Create(
                id: IDs.Images.MembersOnlyImage,
                title: "Members Only",
                albumId: IDs.Albums.MembersOnly,
                authorId: IDs.Users.Member,
                uploaderId: IDs.Users.Member,
                tags: ["members", "exclusive"],
                uploadedAt: DateTime.UtcNow.AddDays(-2),
                accessLevel: AccessLevelValue.AuthReadOnly,
                status: ImageStatusValue.Available,
                removedAt: null,
                likes: [new LikeModel(IDs.Images.MembersOnlyImage, IDs.Users.Admin)]
            ),
        ];
}
