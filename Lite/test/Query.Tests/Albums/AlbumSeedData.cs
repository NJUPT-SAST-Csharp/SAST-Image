using Domain.AlbumAggregate.AlbumEntity;

namespace Query.Albums;

internal sealed class AlbumSeedData
{
    public static IEnumerable<AlbumModel> Seed =>
        [
            AlbumModel.Create(
                id: IDs.Albums.GeneralAlbum,
                title: "Album 1",
                description: "Description 1",
                authorId: IDs.Users.Admin,
                categoryId: IDs.Categories.General,
                accessLevel: AccessLevelValue.PublicReadOnly,
                tags: ["tag1", "tag2"],
                createdAt: DateTime.UtcNow.AddDays(-10),
                updatedAt: DateTime.UtcNow.AddDays(-2),
                removedAt: null,
                subscribes:
                [
                    new SubscribeModel(IDs.Albums.GeneralAlbum, IDs.Users.Member),
                    new SubscribeModel(IDs.Albums.GeneralAlbum, IDs.Users.Photographer),
                ],
                images: []
            ),
            AlbumModel.Create(
                id: IDs.Albums.CityNights,
                title: "City Nights",
                description: "Urban scenes and city lights.",
                authorId: IDs.Users.Photographer,
                categoryId: IDs.Categories.Urban,
                accessLevel: AccessLevelValue.Private,
                tags: ["urban", "night"],
                createdAt: DateTime.UtcNow.AddDays(-6),
                updatedAt: DateTime.UtcNow.AddDays(-1),
                removedAt: null,
                subscribes: [new SubscribeModel(IDs.Albums.CityNights, IDs.Users.Admin)],
                images: []
            ),
            AlbumModel.Create(
                id: IDs.Albums.MembersOnly,
                title: "Members Only",
                description: "Exclusive albums for registered members.",
                authorId: IDs.Users.Member,
                categoryId: IDs.Categories.Nature,
                accessLevel: AccessLevelValue.AuthReadOnly,
                tags: ["members", "exclusive"],
                createdAt: DateTime.UtcNow.AddDays(-4),
                updatedAt: DateTime.UtcNow.AddDays(-2),
                removedAt: null,
                subscribes: [new SubscribeModel(IDs.Albums.MembersOnly, IDs.Users.Admin)],
                images: []
            ),
            AlbumModel.Create(
                id: IDs.Albums.ArchivedAlbum,
                title: "Archived Album",
                description: "Removed album for filtering tests.",
                authorId: IDs.Users.Curator,
                categoryId: IDs.Categories.Portrait,
                accessLevel: AccessLevelValue.PublicReadOnly,
                tags: ["archived"],
                createdAt: DateTime.UtcNow.AddDays(-20),
                updatedAt: DateTime.UtcNow.AddDays(-15),
                removedAt: DateTime.UtcNow.AddDays(-10),
                subscribes: [],
                images: []
            ),
        ];
}
