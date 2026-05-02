namespace Query.Users;

internal sealed class UserSeedData
{
    public static IEnumerable<UserModel> Seed =>
        [
            UserModel.Create(
                id: IDs.Users.Admin,
                username: "user1",
                nickname: "User 1",
                biography: "Bio 1",
                registeredAt: DateTime.UtcNow.AddDays(-10)
            ),
            UserModel.Create(
                id: IDs.Users.Member,
                username: "user2",
                nickname: "User 2",
                biography: "Bio 2",
                registeredAt: DateTime.UtcNow.AddDays(-8)
            ),
            UserModel.Create(
                id: IDs.Users.Photographer,
                username: "photographer",
                nickname: "Photo Pro",
                biography: "Landscape and urban photography.",
                registeredAt: DateTime.UtcNow.AddDays(-5)
            ),
            UserModel.Create(
                id: IDs.Users.Curator,
                username: "curator",
                nickname: "Curator",
                biography: "Curates community albums.",
                registeredAt: DateTime.UtcNow.AddDays(-2)
            ),
        ];
}
