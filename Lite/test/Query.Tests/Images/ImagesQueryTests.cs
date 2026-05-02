using Query.Database;
using Query.Images.Queries;
using Query.Tests;
using Shouldly;

namespace Query.Images;

[TestClass]
public sealed class ImagesQueryTests
{
    private static async ValueTask<ImageDto[]> ExcuteAsync(ImagesQuery query)
    {
        await using var context = QueryDbContext.New;
        var handler = new ImagesQueryHandler(context);
        return await handler.Handle(query, CancellationToken.None);
    }

    [TestMethod]
    public async Task Returns_Public_Read_Images_For_Anonymous()
    {
        var results = await ExcuteAsync(new ImagesQuery(null, null, null, TestActors.Anonymous));

        results.Select(r => r.Id).ShouldBe([IDs.Images.MountainImage, IDs.Images.GeneralImage]);
    }

    [TestMethod]
    public async Task Filters_By_Uploader()
    {
        var results = await ExcuteAsync(
            new ImagesQuery(IDs.Users.Member, null, null, TestActors.Anonymous)
        );

        results.Select(r => r.Id).ShouldBe([IDs.Images.MountainImage]);
    }

    [TestMethod]
    public async Task Filters_By_Album()
    {
        var results = await ExcuteAsync(
            new ImagesQuery(null, IDs.Albums.GeneralAlbum, null, TestActors.Anonymous)
        );

        results.Select(r => r.Id).ShouldBe([IDs.Images.MountainImage, IDs.Images.GeneralImage]);
    }

    [TestMethod]
    public async Task Applies_Cursor_Filter()
    {
        var results = await ExcuteAsync(
            new ImagesQuery(null, null, IDs.Images.MountainImage, TestActors.Anonymous)
        );

        results.Select(r => r.Id).ShouldBe([IDs.Images.GeneralImage]);
    }

    [TestMethod]
    public async Task Returns_Authenticated_Only_Images_For_Authenticated_User()
    {
        var results = await ExcuteAsync(
            new ImagesQuery(null, null, null, TestActors.Authenticated(IDs.Users.Member))
        );

        results
            .Select(r => r.Id)
            .ShouldBe([
                IDs.Images.MembersOnlyImage,
                IDs.Images.MountainImage,
                IDs.Images.GeneralImage,
            ]);
    }

    [TestMethod]
    public async Task Returns_Private_Images_For_Author()
    {
        var results = await ExcuteAsync(
            new ImagesQuery(null, null, null, TestActors.Authenticated(IDs.Users.Photographer))
        );

        results
            .Select(r => r.Id)
            .ShouldBe([
                IDs.Images.MembersOnlyImage,
                IDs.Images.UrbanImage,
                IDs.Images.MountainImage,
                IDs.Images.GeneralImage,
            ]);
    }

    [TestMethod]
    public async Task Returns_Private_Images_For_Admin()
    {
        var results = await ExcuteAsync(
            new ImagesQuery(null, null, null, TestActors.Admin(IDs.Users.Admin))
        );

        results
            .Select(r => r.Id)
            .ShouldBe([
                IDs.Images.MembersOnlyImage,
                IDs.Images.UrbanImage,
                IDs.Images.MountainImage,
                IDs.Images.GeneralImage,
            ]);
    }

    [TestMethod]
    public async Task Private_Images_Are_Not_Visible_To_Other_Users()
    {
        var results = await ExcuteAsync(
            new ImagesQuery(null, null, null, TestActors.Authenticated(IDs.Users.Curator))
        );

        results
            .Select(r => r.Id)
            .ShouldBe([
                IDs.Images.MembersOnlyImage,
                IDs.Images.MountainImage,
                IDs.Images.GeneralImage,
            ]);
    }

    [TestMethod]
    public async Task Removed_Images_Are_Excluded()
    {
        var results = await ExcuteAsync(
            new ImagesQuery(null, IDs.Albums.CityNights, null, TestActors.Admin(IDs.Users.Admin))
        );

        results.Select(r => r.Id).ShouldBe([IDs.Images.UrbanImage]);
    }

    [TestMethod]
    public async Task Includes_Requester_Like_Status()
    {
        var results = await ExcuteAsync(
            new ImagesQuery(
                null,
                IDs.Albums.GeneralAlbum,
                null,
                TestActors.Authenticated(IDs.Users.Member)
            )
        );

        var general = results.Single(r => r.Id == IDs.Images.GeneralImage);
        var mountain = results.Single(r => r.Id == IDs.Images.MountainImage);

        general.Requester.Liked.ShouldBeTrue();
        mountain.Requester.Liked.ShouldBeFalse();
    }
}
