using System.Runtime.CompilerServices;
using Domain.AlbumAggregate;
using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.Commands;
using Domain.AlbumAggregate.Events;
using Domain.AlbumAggregate.Exceptions;
using Domain.AlbumAggregate.ImageEntity;
using Domain.AlbumAggregate.Services;
using Domain.CategoryAggregate.CategoryEntity;
using Domain.Entity;
using Domain.Shared;
using Domain.Tests;
using Domain.Tests.ImageEntity;
using Domain.UserAggregate.UserEntity;
using Moq;
using Shouldly;
using static Domain.Tests.TestActor;

namespace Domain.Tests.AlbumEntity;

[TestClass]
public class AlbumTests(TestContext context)
{
    #region Create
    [TestMethod]
    public async Task Add_New_Album_When_Created()
    {
        List<Album> db = [];
        var command = new CreateAlbumCommand(
            AlbumTitle.New,
            AlbumDescription.New,
            AccessLevel.Default,
            CategoryId.New,
            Actor.New(AuthorId)
        );
        var categoryCheckerMock = new Mock<ICategoryExistenceChecker>();
        var repositoryMock = new Mock<IAlbumRepository>();
        var cancellationToken = context.CancellationToken;

        categoryCheckerMock
            .Setup(c => c.CheckAsync(command.CategoryId, cancellationToken))
            .Returns(Task.CompletedTask);

        repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Album>(), cancellationToken))
            .Callback<Album, CancellationToken>((album, _) => db.Add(album))
            .Returns(Task.CompletedTask);

        var id = await Album.CreateAsync(
            command,
            categoryCheckerMock.Object,
            repositoryMock.Object,
            cancellationToken
        );

        db.Count.ShouldBe(1);
        var album = db[0];
        album.Id.ShouldBe(id);
        album.DomainEvents.Count.ShouldBe(1);
        album.DomainEvents.First().ShouldBeOfType<AlbumCreatedEvent>();
        categoryCheckerMock.Verify(
            c => c.CheckAsync(command.CategoryId, cancellationToken),
            Times.Once
        );
        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Album>(), cancellationToken), Times.Once);
    }

    #endregion

    #region IsRemoved

    [TestMethod]
    public void Return_True_When_Album_Removed()
    {
        var album = Album.Removed;

        bool isRemoved = album.IsRemoved;

        isRemoved.ShouldBeTrue();
    }

    [TestMethod]
    public void Return_False_When_Album_Not_Removed()
    {
        var album = Album.New;

        album.IsRemoved.ShouldBeFalse();
    }

    #endregion

    #region UpdateInfo

    [TestMethod]
    public void Throw_When_UpdateInfo_In_Immutable_Album()
    {
        var album = Album.Removed;
        UpdateAlbumInfoCommand command = new(
            AlbumId.New,
            AlbumTitle.New,
            AlbumDescription.New,
            AlbumTags.Empty,
            Actor.Author
        );

        Should.Throw<AlbumRemovedException>(() => album.UpdateInfo(command));
    }

    [DataRow(VisitorId)]
    [TestMethod]
    public void Throw_When_UpdateInfo_As_Not_Author_Or_Admin(long actorId)
    {
        var album = Album.New;
        UpdateAlbumInfoCommand command = new(
            AlbumId.New,
            AlbumTitle.New,
            AlbumDescription.New,
            AlbumTags.Empty,
            Actor.New(actorId)
        );

        Should.Throw<NoPermissionException>(() => album.UpdateInfo(command));
    }

    [DataRow(AdminId, true)]
    [DataRow(AuthorId, false)]
    [TestMethod]
    public void Raise_Event_When_Info_Updated(long actorId, bool isAdmin)
    {
        var album = Album.New;
        UpdateAlbumInfoCommand command = new(
            AlbumId.New,
            AlbumTitle.New,
            AlbumDescription.New,
            AlbumTags.Empty,
            Actor.New(actorId, isAdmin)
        );

        album.UpdateInfo(command);

        album.DomainEvents.Count.ShouldBe(1);
        album.DomainEvents.First().ShouldBeOfType<AlbumInfoUpdatedEvent>();
    }

    #endregion

    #region UpdateCategory

    [TestMethod]
    public void Throw_When_UpdateCategory_In_Immutable_Album()
    {
        var album = Album.Removed;
        UpdateAlbumCategoryCommand command = new(AlbumId.New, CategoryId.New, Actor.Author);
        var checker = new Mock<ICategoryExistenceChecker>();
        checker
            .Setup(c => c.CheckAsync(command.Category, context.CancellationToken))
            .Returns(Task.CompletedTask);

        Should.Throw<AlbumRemovedException>(async () =>
            await album.UpdateCategory(command, checker.Object)
        );
    }

    [DataRow(VisitorId)]
    [TestMethod]
    public void Throw_When_UpdateCategory_As_Not_Author_Or_Admin(long actorId)
    {
        var album = Album.New;
        UpdateAlbumCategoryCommand command = new(AlbumId.New, CategoryId.New, Actor.New(actorId));
        var checker = new Mock<ICategoryExistenceChecker>();
        checker
            .Setup(c => c.CheckAsync(command.Category, context.CancellationToken))
            .Returns(Task.CompletedTask);

        Should.Throw<NoPermissionException>(async () =>
            await album.UpdateCategory(command, checker.Object)
        );
    }

    [DataRow(AdminId, true)]
    [DataRow(AuthorId, false)]
    [TestMethod]
    public async Task Raise_Event_When_Category_Updated(long actorId, bool isAdmin)
    {
        var album = Album.New;
        UpdateAlbumCategoryCommand command = new(
            AlbumId.New,
            CategoryId.New,
            Actor.New(actorId, isAdmin)
        );

        var checker = new Mock<ICategoryExistenceChecker>();
        checker
            .Setup(c => c.CheckAsync(command.Category, context.CancellationToken))
            .Returns(Task.CompletedTask);

        await album.UpdateCategory(command, checker.Object);

        album.DomainEvents.Count.ShouldBe(1);
        album.DomainEvents.First().ShouldBeOfType<AlbumCategoryUpdatedEvent>();
    }

    #endregion

    #region UpdateAccessLevel

    [TestMethod]
    public void Throw_When_UpdateAccessLevel_In_Removed_Album()
    {
        var album = Album.Removed;
        UpdateAccessLevelCommand command = new(AlbumId.New, AccessLevel.Default, Actor.Author);

        Should.Throw<AlbumRemovedException>(() => album.UpdateAccessLevel(command));
    }

    [DataRow(VisitorId)]
    [TestMethod]
    public void Throw_When_UpdateAccessLevel_As_Not_Author_Or_Admin(long actorId)
    {
        var album = Album.New;
        UpdateAccessLevelCommand command = new(
            AlbumId.New,
            AccessLevel.Default,
            Actor.New(actorId)
        );

        Should.Throw<NoPermissionException>(() => album.UpdateAccessLevel(command));
    }

    [DataRow(AdminId, true)]
    [DataRow(AuthorId, false)]
    [TestMethod]
    public void Raise_Event_When_AccessLevel_Updated(long actorId, bool isAdmin)
    {
        var album = Album.New;
        UpdateAccessLevelCommand command = new(
            AlbumId.New,
            AccessLevel.Default,
            Actor.New(actorId, isAdmin)
        );

        album.UpdateAccessLevel(command);

        album.DomainEvents.Count.ShouldBe(1);
        album.DomainEvents.First().ShouldBeOfType<AlbumAccessLevelUpdatedEvent>();
    }

    #endregion

    #region UpdateCover

    [TestMethod]
    public void Throw_When_UpdateCover_In_Immutable_Album()
    {
        var album = Album.Removed;
        UpdateCoverCommand command = new(AlbumId.New, null, Actor.Author);

        Should.Throw<AlbumRemovedException>(() => album.UpdateCover(command));
    }

    [DataRow(VisitorId)]
    [TestMethod]
    public void Throw_When_UpdateCover_As_Not_Author_Or_Admin(long actorId)
    {
        var album = Album.New;
        UpdateCoverCommand command = new(AlbumId.New, null, Actor.New(actorId));

        Should.Throw<NoPermissionException>(() => album.UpdateCover(command));
    }

    [DataRow(AdminId, true)]
    [DataRow(AuthorId, false)]
    [TestMethod]
    public void Raise_Event_When_Cover_Updated(long actorId, bool isAdmin)
    {
        var album = Album.New;
        UpdateCoverCommand command = new(
            AlbumId.New,
            ImageFile.Default,
            Actor.New(actorId, isAdmin)
        );

        album.UpdateCover(command);

        album.DomainEvents.Count.ShouldBe(1);
        album.DomainEvents.First().ShouldBeOfType<AlbumCoverUpdatedManuallyEvent>();
    }

    #endregion

    #region AddImage

    [TestMethod]
    public void Throw_When_AddImage_In_Immutable_Album()
    {
        var album = Album.Removed;
        AddImageCommand command = new(
            AlbumId.New,
            ImageTitle.New,
            ImageTags.New,
            ImageFile.Default,
            Actor.Author
        );

        Should.Throw<AlbumRemovedException>(() => album.AddImage(command));
    }

    [TestMethod]
    public void Throw_When_AddImage_As_Not_Author_Or_Admin()
    {
        var album = Album.New;
        AddImageCommand command = new(
            AlbumId.New,
            ImageTitle.New,
            ImageTags.New,
            ImageFile.Default,
            Actor.New(VisitorId)
        );

        Should.Throw<NoPermissionException>(() => album.AddImage(command));
    }

    [DataRow(AuthorId, false)]
    [DataRow(AdminId, true)]
    [TestMethod]
    public void Raise_Event_When_Image_Added(long actorId, bool isAdmin)
    {
        var album = Album.New;
        int count = album.Images.Count;
        AddImageCommand command = new(
            AlbumId.New,
            ImageTitle.New,
            ImageTags.New,
            ImageFile.Default,
            Actor.New(actorId, isAdmin)
        );

        album.AddImage(command);

        album.DomainEvents.Count.ShouldBeGreaterThan(0);
        album.Images.Count.ShouldBe(count + 1);
    }

    [TestMethod]
    public void Raise_AlbumCoverUpdatedEvent_When_IsLatestImage_As_Image_Added()
    {
        var album = Album.New;
        AddImageCommand command = new(
            AlbumId.New,
            ImageTitle.New,
            ImageTags.New,
            ImageFile.Default,
            Actor.Author
        );

        album.AddImage(command);

        album.DomainEvents.Count.ShouldBe(2);
        album.DomainEvents.ShouldBeOfTypes(
            typeof(ImageAddedEvent),
            typeof(AlbumCoverUpdatedAutomaticallyEvent)
        );
    }

    [TestMethod]
    public void Not_Raise_AlbumCoverUpdatedEvent_When_Not_IsLatestImage_As_Image_Added()
    {
        var album = Album.New;
        album.CustomCover = true;
        AddImageCommand command = new(
            AlbumId.New,
            ImageTitle.New,
            ImageTags.New,
            ImageFile.Default,
            Actor.Author
        );

        album.AddImage(command);

        album.DomainEvents.Count.ShouldBe(1);
        album.DomainEvents.First().ShouldBeOfType<ImageAddedEvent>();
    }

    #endregion

    #region RemoveImage


    [TestMethod]
    public void Throw_When_RemoveImage_In_Immutable_Album()
    {
        var album = Album.Removed;
        RemoveImageCommand command = new(AlbumId.New, album.Images.Random.Id, Actor.Author);

        Should.Throw<AlbumRemovedException>(() => album.RemoveImage(command));
    }

    [TestMethod]
    public void Throw_When_RemoveImage_As_Not_Author_Or_Admin()
    {
        var album = Album.New;
        RemoveImageCommand command = new(AlbumId.New, album.Images.Random.Id, Actor.New(VisitorId));

        Should.Throw<NoPermissionException>(() => album.RemoveImage(command));
    }

    [TestMethod]
    public void Raise_CoverUpdatedEvent_When_Image_As_LatestImage_Removed()
    {
        var imageToBeRemoved = Image.New(new(5));
        var imageOnSecondPlace = Image.New(new(4));
        List<Image> images = [imageOnSecondPlace, imageToBeRemoved, Image.New(new(2))];
        var album = Album.New;
        album.Images = images;

        RemoveImageCommand command = new(AlbumId.New, imageToBeRemoved.Id, Actor.Author);

        album.RemoveImage(command);

        album
            .DomainEvents.ShouldHaveSingleItem()
            .ShouldBeOfType<AlbumCoverUpdatedAutomaticallyEvent>()
            .Image.ShouldBe(imageOnSecondPlace.Id);
    }

    [TestMethod]
    public void Not_Raise_CoverUpdatedEvent_When_Image_Not_As_LatestImage_Removed()
    {
        var imageToBeRemoved = Image.New(new(4));
        List<Image> images = [Image.New(new(5)), imageToBeRemoved, Image.New(new(3))];
        var album = Album.New;
        album.Images = images;

        RemoveImageCommand command = new(AlbumId.New, imageToBeRemoved.Id, Actor.Author);

        album.RemoveImage(command);

        album.DomainEvents.Count.ShouldBe(0);
    }

    #endregion

    #region RestoreImage

    [TestMethod]
    public void Throw_When_RestoreImage_In_Immutable_Album()
    {
        var album = Album.Removed;
        RestoreImageCommand command = new(AlbumId.New, album.Images.Random.Id, Actor.Author);

        Should.Throw<AlbumRemovedException>(() => album.RestoreImage(command));
    }

    [TestMethod]
    public void Throw_When_RestoreImage_As_Not_Author_Or_Admin()
    {
        var album = Album.New;
        RestoreImageCommand command = new(
            AlbumId.New,
            album.Images.Random.Id,
            Actor.New(VisitorId)
        );

        Should.Throw<NoPermissionException>(() => album.RestoreImage(command));
    }

    [TestMethod]
    public void Raise_CoverUpdatedEvent_When_Image_As_LatestImage_Restored()
    {
        var imageToBeRestored = Image.Removed(new(5));
        List<Image> images = [Image.New(new(2)), imageToBeRestored, Image.New(new(3))];
        var album = Album.New;
        album.Images = images;
        RestoreImageCommand command = new(AlbumId.New, imageToBeRestored.Id, Actor.Author);

        album.RestoreImage(command);

        album
            .DomainEvents.ShouldHaveSingleItem()
            .ShouldBeOfType<AlbumCoverUpdatedAutomaticallyEvent>()
            .Image.ShouldBe(imageToBeRestored.Id);
    }

    [TestMethod]
    public void Not_Raise_CoverUpdatedEvent_When_Image_Not_As_LatestImage_Restored()
    {
        var imageToBeRestored = Image.Removed(new(4));
        List<Image> images = [Image.New(new(5)), imageToBeRestored, Image.New(new(3))];
        var album = Album.New;
        album.Images = images;
        RestoreImageCommand command = new(AlbumId.New, imageToBeRestored.Id, Actor.Author);

        album.RestoreImage(command);

        album.DomainEvents.Count.ShouldBe(0);
    }

    #endregion

    #region Remove

    [TestMethod]
    public void Not_Change_When_Album_Already_Removed()
    {
        var album = Album.Removed;
        RemoveAlbumCommand command = new(AlbumId.New, Actor.Author);

        album.Remove(command);

        album.DomainEvents.Count.ShouldBe(0);
        album.IsRemoved.ShouldBeTrue();
    }

    [TestMethod]
    public void Raise_Event_When_Removed()
    {
        var album = Album.New;
        RemoveAlbumCommand command = new(AlbumId.New, Actor.Author);

        album.Remove(command);

        album.DomainEvents.Count.ShouldBe(1);
        album.DomainEvents.First().ShouldBeOfType<AlbumRemovedEvent>();
    }

    [TestMethod]
    public void Update_Contained_Images_Status_When_Removed()
    {
        var album = Album.New;
        RemoveAlbumCommand command = new(AlbumId.New, Actor.Author);

        album.Remove(command);

        foreach (var image in album.Images.Where(i => i.Status.IsRemoved is false))
        {
            image.Status.Value.ShouldBe(ImageStatusValue.AlbumRemoved);
        }
    }

    #endregion

    #region Restore

    [TestMethod]
    public void Not_Change_When_Album_Available()
    {
        var album = Album.New;
        RestoreAlbumCommand command = new(AlbumId.New, Actor.Author);

        album.Restore(command);

        album.DomainEvents.Count.ShouldBe(0);
    }

    [TestMethod]
    public void Raise_Event_When_Restored()
    {
        var album = Album.Removed;
        RestoreAlbumCommand command = new(AlbumId.New, Actor.Author);

        album.Restore(command);

        album.DomainEvents.Count.ShouldBe(1);
        album.DomainEvents.First().ShouldBeOfType<AlbumRestoredEvent>();
    }

    [TestMethod]
    public void Update_Contained_Images_Status_When_Restored()
    {
        var album = Album.Removed;
        RestoreAlbumCommand command = new(AlbumId.New, Actor.Author);

        album.Restore(command);

        foreach (var image in album.Images.Where(i => i.Status.IsRemoved is false))
        {
            image.Status.Value.ShouldBe(ImageStatusValue.Available);
        }
    }

    #endregion
}

internal static class TestAlbum
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    private static extern Album Constructor();

    extension(Album album)
    {
        private T GetValue<T>() => album.GetValue<Album, T>();

        public UserId Author => album.GetValue<UserId>();

        public bool IsRemoved => album.GetValue<bool>();
        public bool CustomCover
        {
            get => album.GetValue<Album, bool>("_customCover");
            set => album.SetValue("_customCover", value);
        }

        public List<Image> Images
        {
            get => album.GetValue<List<Image>>();
            set => album.SetValue(value);
        }

        public static Album New
        {
            get
            {
                var a = Constructor();

                a.Images =
                [
                    Image.New(new(1)),
                    Image.New(new(2)),
                    Image.Removed(new(3)),
                    Image.New(new(4)),
                ];
                a.SetValue(Actor.Author.Id);
                a.SetValue(Subscribe.Default(a.Id));

                a.SetId(AlbumId.New);

                return a;
            }
        }

        public static Album Removed
        {
            get
            {
                var a = Album.New;
                a.SetValue(true);

                return a;
            }
        }
    }
}

file static class SubscribeTestHelper
{
    extension(Subscribe)
    {
        public static Subscribe New(AlbumId albumId, UserId userId) => new(albumId, userId);

        public static List<Subscribe> Default(AlbumId album) =>
            new([Subscribe.New(album, new(123)), Subscribe.New(album, new(321))]);
    }
}

file static class AlbumTitleTestHelper
{
    extension(AlbumTitle)
    {
        public static AlbumTitle New =>
            new(Random.Shared.Chars(AlbumTitle.MinLength, AlbumTitle.MaxLength));
    }
}

file static class AlbumDescriptionTestHelper
{
    extension(AlbumDescription)
    {
        public static AlbumDescription New =>
            new(Random.Shared.Chars(AlbumDescription.MinLength, AlbumDescription.MaxLength));
    }
}

file static class CategoryIdTestHelper
{
    extension(CategoryId)
    {
        public static CategoryId New => new(Random.Shared.NextInt64(1, long.MaxValue));
    }
}

file static class ImageTitleTestHelper
{
    extension(ImageTitle)
    {
        public static ImageTitle New => new(Random.Shared.Chars(0, ImageTitle.MaxLength));
    }
}

file static class ImageTagsTestHelper
{
    extension(ImageTags)
    {
        public static ImageTags New =>
            new([
                .. Enumerable
                    .Repeat(
                        () => Random.Shared.Chars(default, ImageTags.MaxLength),
                        Random.Shared.Next(1, ImageTags.MaxCount)
                    )
                    .Select(f => f()),
            ]);
    }
}

file static class ImageFileTestHelper
{
    extension(ImageFile)
    {
        public static ImageFile Default => default;
    }
}

file static class AccessLevelHelper
{
    extension(AccessLevel)
    {
        public static AccessLevel Default => AccessLevel.AuthReadOnly;
    }
}
