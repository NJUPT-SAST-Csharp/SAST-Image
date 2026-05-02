using Domain.AlbumAggregate.ImageEntity;
using Shouldly;

namespace Domain.Tests.ImageEntity;

[TestClass]
public class ImageTitleTests
{
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [TestMethod]
    public void Should_Set_Empty_When_Create_From_NullOrWhiteSpace(string value)
    {
        ImageTitle.TryCreateNew(value, out var image);

        image.Value.ShouldBe(string.Empty);
    }

    private static readonly IEnumerable<object[]> valid_values =
    [
        [""],
        ["  "],
        ["image"],
        ["图片"],
        ["🐰🦊🐺🐻‍❄️"],
        [string.Empty.PadRight(ImageTitle.MaxLength, 'a')],
        [string.Empty.PadRight(ImageTitle.MaxLength - 1, 'a')],
    ];

    [TestMethod]
    [DynamicData(nameof(valid_values))]
    public void Return_True_When_Create_From_Valid(string value)
    {
        bool result = ImageTitle.TryCreateNew(value, out var _);

        result.ShouldBeTrue();
    }

    [TestMethod]
    public void Should_Trim_Whitespace_When_Create()
    {
        const string input_value = "  image  ";
        const string actual_value = "image";

        ImageTitle.TryCreateNew(input_value, out var image);

        image.Value.ShouldBe(actual_value);
    }

    [TestMethod]
    public void Should_Set_Value_When_Create_From_Valid()
    {
        const string value = "image";

        ImageTitle.TryCreateNew(value, out var image);

        image.Value.ShouldBe(value);
    }

    [TestMethod]
    public void Should_Be_Equal_When_Same_Value()
    {
        const string value = "image";

        ImageTitle.TryCreateNew(value, out var image1);
        ImageTitle.TryCreateNew(value, out var image2);

        image1.ShouldBe(image2);
    }

    [TestMethod]
    public void Should_Be_Equal_When_Same_Value_With_Whitespace()
    {
        const string value1 = "image";
        const string value2 = "image ";

        ImageTitle.TryCreateNew(value1, out var image1);
        ImageTitle.TryCreateNew(value2, out var image2);

        image1.ShouldBe(image2);
    }

    [TestMethod]
    public void Should_Not_Be_Equal_When_Different_Value()
    {
        const string value1 = "image1";
        const string value2 = "image2";

        ImageTitle.TryCreateNew(value1, out var image1);
        ImageTitle.TryCreateNew(value2, out var image2);

        image1.ShouldNotBe(image2);
    }
}

internal static class TestImageTitle
{
    extension(ImageTitle)
    {
        public static ImageTitle New => new("test_title");
    }
}
