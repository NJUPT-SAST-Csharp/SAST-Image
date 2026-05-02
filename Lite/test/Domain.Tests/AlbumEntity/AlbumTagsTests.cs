using Domain.AlbumAggregate.AlbumEntity;
using Shouldly;

namespace Domain.Tests.AlbumEntity;

[TestClass]
public class AlbumTagsTests
{
    [TestMethod]
    public void Return_False_When_Too_Many_Elements()
    {
        string[] albumTags_more_than_MaxCount =
        [
            .. Enumerable.Range(default, AlbumTags.MaxCount + 1).Select(index => index.ToString()),
        ];

        bool result = AlbumTags.TryCreateNew(albumTags_more_than_MaxCount, out var albumTags);

        result.ShouldBeFalse();
    }

    [TestMethod]
    public void Return_True_When_Create_From_Valid()
    {
        const int albumTag_count = AlbumTags.MaxCount - 1;

        string[] valid_albumTags =
        [
            .. Enumerable.Range(default, AlbumTags.MaxCount - 1).Select(index => index.ToString()),
        ];

        AlbumTags.TryCreateNew(valid_albumTags, out var albumTags);

        Assert.IsNotNull(albumTags);
        albumTags.Value.Length.ShouldBe(albumTag_count);
    }

    [TestMethod]
    public void Return_True_When_Count_Valid_After_Remove_Duplicate()
    {
        const int duplicate_id = 0;
        const int duplicate_count = 10;
        var albumTags_less_than_MaxCount = Enumerable
            .Range(default, AlbumTags.MaxCount - 1)
            .Select(index => index.ToString());
        var duplicate_albumTags = Enumerable.Repeat(duplicate_id.ToString(), duplicate_count);
        string[] total_albumTags = [.. albumTags_less_than_MaxCount, .. duplicate_albumTags];

        bool result = AlbumTags.TryCreateNew(total_albumTags, out var _);

        result.ShouldBeTrue();
    }

    [TestMethod]
    public void Should_Not_Contain_Duplicate_Elements()
    {
        const int duplicate_id = 0;
        const int duplicate_count = AlbumTags.MaxCount / 2;
        string[] albumTags_with_duplicate_ones =
        [
            .. Enumerable.Repeat(duplicate_id.ToString(), duplicate_count),
        ];

        _ = AlbumTags.TryCreateNew(albumTags_with_duplicate_ones, out var albumTags);

        Assert.IsNotNull(albumTags);
        albumTags.Value.ShouldBeUnique();
    }

    [TestMethod]
    public void Should_Have_Same_Count_When_Create_From_No_Duplicate()
    {
        const int albumTag_count = AlbumTags.MaxCount - 1;

        string[] valid_albumTags =
        [
            .. Enumerable.Range(default, AlbumTags.MaxCount - 1).Select(index => index.ToString()),
        ];

        AlbumTags.TryCreateNew(valid_albumTags, out var albumTags);

        Assert.IsNotNull(albumTags);
        albumTags.Value.Length.ShouldBe(albumTag_count);
    }

    [TestMethod]
    public void Should_Equal_When_Create_From_Same_Set_With_No_Order()
    {
        string[] albumTags =
        [
            .. Enumerable.Range(default, AlbumTags.MaxCount - 1).Select(index => index.ToString()),
        ];

        AlbumTags.TryCreateNew(albumTags, out var albumTags1);
        Random.Shared.Shuffle(albumTags);
        AlbumTags.TryCreateNew(albumTags, out var albumTags2);
        Assert.IsNotNull(albumTags1);
        Assert.IsNotNull(albumTags2);

        albumTags1.Equals(albumTags2).ShouldBeTrue();
        (albumTags1 == albumTags2).ShouldBeTrue();
        EqualityComparer<AlbumTags>.Default.Equals(albumTags1, albumTags2).ShouldBeTrue();
    }

    [TestMethod]
    public void Return_True_When_Create_From_Empty()
    {
        string[] empty_albumTags = [.. new AlbumTags().Value.Select(t => t.ToString())];

        bool result = AlbumTags.TryCreateNew(empty_albumTags, out var _);

        result.ShouldBeTrue();
    }

    [TestMethod]
    public void Return_True_When_Create_From_MaxCount()
    {
        string[] maxcount_albumTags =
        [
            .. Enumerable.Range(default, AlbumTags.MaxCount).Select(index => index.ToString()),
        ];

        bool result = AlbumTags.TryCreateNew(maxcount_albumTags, out var _);

        result.ShouldBeTrue();
    }

    [TestMethod]
    public void Return_False_When_Any_Tag_Exceeds_MaxLength()
    {
        string too_long_tag = string.Empty.PadRight(AlbumTags.MaxLength + 1, 'a');
        string[] albumTags = ["tag", too_long_tag];

        bool result = AlbumTags.TryCreateNew(albumTags, out var _);

        result.ShouldBeFalse();
    }

    [TestMethod]
    public void Should_Ignore_Null_Or_Whitespace_Elements_When_Create()
    {
        string[] albumTags = ["rock", "", "   ", "jazz", "\t", "rock"];

        bool result = AlbumTags.TryCreateNew(albumTags, out var created);

        result.ShouldBeTrue();
        Assert.IsNotNull(created);
        created.Value.ShouldBeUnique();
        created.Value.Length.ShouldBe(2);
    }
}
