using Query.Albums.Queries;
using Query.Database;
using Query.Tests;
using Shouldly;

namespace Query.Albums;

[TestClass]
public sealed class AlbumsQueryTests
{
    private static async ValueTask<AlbumDto[]> ExcuteAsync(AlbumsQuery query)
    {
        await using var context = QueryDbContext.New;
        var handler = new AlbumsQueryHandler(context);
        return await handler.Handle(query, CancellationToken.None);
    }

    [TestMethod]
    public async Task Returns_Public_Read_Albums_For_Anonymous()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(null, null, null, null, TestActors.Anonymous)
        );

        results.Select(r => r.Id).ShouldBe([IDs.Albums.GeneralAlbum]);
        results.ShouldAllBe(a =>
            a.AccessLevel >= Domain.AlbumAggregate.AlbumEntity.AccessLevelValue.PublicReadOnly
        );
    }

    [TestMethod]
    public async Task Filters_By_Category()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(
                IDs.Categories.Urban,
                null,
                null,
                null,
                TestActors.Admin(IDs.Users.Admin)
            )
        );

        results.Length.ShouldBe(1);
        results[0].Id.ShouldBe(IDs.Albums.CityNights);
    }

    [TestMethod]
    public async Task Filters_By_Author()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(null, IDs.Users.Admin, null, null, TestActors.Anonymous)
        );

        results.Length.ShouldBe(1);
        results[0].Id.ShouldBe(IDs.Albums.GeneralAlbum);
    }

    [TestMethod]
    public async Task Filters_By_Title()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(null, null, "City", null, TestActors.Admin(IDs.Users.Admin))
        );

        results.Length.ShouldBe(1);
        results[0].Id.ShouldBe(IDs.Albums.CityNights);
    }

    [TestMethod]
    public async Task Applies_Cursor_Filter()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(null, null, null, IDs.Albums.CityNights, TestActors.Anonymous)
        );

        results.Length.ShouldBe(1);
        results[0].Id.ShouldBe(IDs.Albums.GeneralAlbum);
    }

    [TestMethod]
    public async Task Returns_Authenticated_Only_Albums_For_Authenticated_User()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(null, null, null, null, TestActors.Authenticated(IDs.Users.Member))
        );

        results.Select(r => r.Id).ShouldBe([IDs.Albums.MembersOnly, IDs.Albums.GeneralAlbum]);
    }

    [TestMethod]
    public async Task Returns_Private_Albums_For_Author()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(
                null,
                null,
                null,
                null,
                TestActors.Authenticated(IDs.Users.Photographer)
            )
        );

        results
            .Select(r => r.Id)
            .ShouldBe([IDs.Albums.MembersOnly, IDs.Albums.CityNights, IDs.Albums.GeneralAlbum]);
    }

    [TestMethod]
    public async Task Returns_Private_Albums_For_Admin()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(null, null, null, null, TestActors.Admin(IDs.Users.Admin))
        );

        results
            .Select(r => r.Id)
            .ShouldBe([IDs.Albums.MembersOnly, IDs.Albums.CityNights, IDs.Albums.GeneralAlbum]);
    }

    [TestMethod]
    public async Task Private_Albums_Are_Not_Visible_To_Other_Users()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(null, null, null, null, TestActors.Authenticated(IDs.Users.Curator))
        );

        results.Select(r => r.Id).ShouldBe([IDs.Albums.MembersOnly, IDs.Albums.GeneralAlbum]);
    }

    [TestMethod]
    public async Task Removed_Albums_Are_Excluded()
    {
        var results = await ExcuteAsync(
            new AlbumsQuery(IDs.Categories.Portrait, null, null, null, TestActors.Anonymous)
        );

        results.ShouldBeEmpty();
    }
}
