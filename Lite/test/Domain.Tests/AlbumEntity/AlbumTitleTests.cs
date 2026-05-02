using Domain.AlbumAggregate.AlbumEntity;
using Shouldly;

namespace Domain.Tests.AlbumEntity;

[TestClass]
public class AlbumTitleTests
{
    private static readonly IEnumerable<object?[]> invalid_values =
    [
        [null],
        [""],
        ["   "],
        [string.Empty.PadRight(AlbumTitle.MaxLength + 1, 'a')],
        [string.Empty.PadRight(AlbumTitle.MinLength - 1, 'a')],
    ];

    [TestMethod]
    [DynamicData(nameof(invalid_values))]
    public void Return_False_When_Create_From_Invalid(string value)
    {
        bool result = AlbumTitle.TryCreateNew(value, out var _);

        result.ShouldBeFalse();
    }

    private static readonly IEnumerable<object[]> valid_values =
    [
        ["album"],
        ["相册"],
        ["🐰🦊🐺🐻‍❄️"],
        [string.Empty.PadRight(AlbumTitle.MaxLength, 'a')],
        [string.Empty.PadRight(AlbumTitle.MaxLength - 1, 'a')],
        [string.Empty.PadRight(AlbumTitle.MinLength, 'a')],
        [string.Empty.PadRight(AlbumTitle.MinLength + 1, 'a')],
    ];

    [TestMethod]
    [DynamicData(nameof(valid_values))]
    public void Return_True_When_Create_From_Valid(string value)
    {
        bool result = AlbumTitle.TryCreateNew(value, out var _);

        result.ShouldBeTrue();
    }

    [DataRow("album  ")]
    [DataRow("   album")]
    [DataRow("   album  ")]
    [TestMethod]
    public void Should_Trim_Whitespace_When_Create(string input_value)
    {
        const string actual_value = "album";

        AlbumTitle.TryCreateNew(input_value, out var album);

        album.Value.ShouldBe(actual_value);
    }

    [TestMethod]
    public void Should_Set_Value_When_Create_From_Valid()
    {
        const string value = "album";

        AlbumTitle.TryCreateNew(value, out var album);

        album.Value.ShouldBe(value);
    }

    [TestMethod]
    public void Should_Be_Equal_When_Same_Value()
    {
        const string value = "album";

        AlbumTitle.TryCreateNew(value, out var album1);
        AlbumTitle.TryCreateNew(value, out var album2);

        album1.ShouldBe(album2);
    }

    [DataRow("album  ")]
    [DataRow("   album")]
    [DataRow("   album  ")]
    [TestMethod]
    public void Should_Be_Equal_When_Same_Value_With_Whitespace(string value_with_whitespace)
    {
        const string value = "album";

        AlbumTitle.TryCreateNew(value, out var album1);
        AlbumTitle.TryCreateNew(value_with_whitespace, out var album2);

        album1.ShouldBe(album2);
    }

    [TestMethod]
    public void Should_Not_Be_Equal_When_Different_Value()
    {
        const string value1 = "album1";
        const string value2 = "album2";

        AlbumTitle.TryCreateNew(value1, out var album1);
        AlbumTitle.TryCreateNew(value2, out var album2);

        album1.ShouldNotBe(album2);
    }
}
