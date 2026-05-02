using Domain.UserAggregate.UserEntity;
using Shouldly;

namespace Domain.Tests.UserEntity;

[TestClass]
public sealed class NicknameTests
{
    private static readonly IEnumerable<object?[]> invalid_values =
    [
        [null],
        [""],
        ["   "],
        [string.Empty.PadRight(Nickname.MaxLength + 1, 'a')],
        [string.Empty.PadRight(Nickname.MinLength - 1, 'a')],
    ];

    [TestMethod]
    [DynamicData(nameof(invalid_values))]
    public void Return_False_When_Create_From_Invalid(string value)
    {
        bool result = Nickname.TryCreateNew(value, out var _);

        result.ShouldBeFalse();
    }

    private static readonly IEnumerable<object[]> valid_values =
    [
        ["bruh"],
        ["昵称"],
        ["🐰🦊🐺🐻‍❄️"],
        [string.Empty.PadRight(Nickname.MaxLength, 'a')],
        [string.Empty.PadRight(Nickname.MaxLength - 1, 'a')],
        [string.Empty.PadRight(Nickname.MinLength, 'a')],
        [string.Empty.PadRight(Nickname.MinLength + 1, 'a')],
    ];

    [TestMethod]
    [DynamicData(nameof(valid_values))]
    public void Return_True_When_Create_From_Valid(string value)
    {
        bool result = Nickname.TryCreateNew(value, out var _);

        result.ShouldBeTrue();
    }

    [DataRow("bruh  ")]
    [DataRow("   bruh")]
    [DataRow("   bruh  ")]
    [TestMethod]
    public void Should_Trim_Whitespace_When_Create(string input_value)
    {
        const string actual_value = "bruh";

        Nickname.TryCreateNew(input_value, out var bruh);

        bruh.Value.ShouldBe(actual_value);
    }

    [TestMethod]
    public void Should_Set_Value_When_Create_From_Valid()
    {
        const string value = "bruh";

        Nickname.TryCreateNew(value, out var bruh);

        bruh.Value.ShouldBe(value);
    }

    [TestMethod]
    public void Should_Be_Equal_When_Same_Value()
    {
        const string value = "bruh";

        Nickname.TryCreateNew(value, out var bruh1);
        Nickname.TryCreateNew(value, out var bruh2);

        bruh1.ShouldBe(bruh2);
    }

    [DataRow("bruh  ")]
    [DataRow("   bruh")]
    [DataRow("   bruh  ")]
    [TestMethod]
    public void Should_Be_Equal_When_Same_Value_With_Whitespace(string value_with_whitespace)
    {
        const string value = "bruh";

        Nickname.TryCreateNew(value, out var bruh1);
        Nickname.TryCreateNew(value_with_whitespace, out var bruh2);

        bruh1.ShouldBe(bruh2);
    }

    [TestMethod]
    public void Should_Not_Be_Equal_When_Different_Value()
    {
        const string value1 = "bruh1";
        const string value2 = "bruh2";

        Nickname.TryCreateNew(value1, out var bruh1);
        Nickname.TryCreateNew(value2, out var bruh2);

        bruh1.ShouldNotBe(bruh2);
    }
}
