using Domain.UserAggregate.UserEntity;
using Shouldly;

namespace Domain.Tests.UserEntity;

[TestClass]
public sealed class UsernameTests
{
    private static readonly IEnumerable<object?[]> invalid_values =
    [
        [null],
        [""],
        ["    "],
        ["瓜瓜"],
        ["user!"],
        [string.Empty.PadRight(Username.MaxLength + 1, 'a')],
        [string.Empty.PadRight(Username.MinLength - 1, 'a')],
    ];

    [TestMethod]
    [DynamicData(nameof(invalid_values))]
    public void Return_False_When_Create_From_Invalid(string value)
    {
        bool result = Username.TryCreateNew(value, out var _);

        result.ShouldBeFalse();
    }

    private static readonly IEnumerable<object[]> valid_values =
    [
        ["user"],
        [string.Empty.PadRight(Username.MaxLength, 'a')],
        [string.Empty.PadRight(Username.MaxLength - 1, 'a')],
        [string.Empty.PadRight(Username.MinLength, 'a')],
        [string.Empty.PadRight(Username.MinLength + 1, 'a')],
    ];

    [TestMethod]
    [DynamicData(nameof(valid_values))]
    public void Return_True_When_Create_From_Valid(string value)
    {
        bool result = Username.TryCreateNew(value, out var _);

        result.ShouldBeTrue();
    }

    [DataRow("user  ")]
    [DataRow("   user")]
    [DataRow("   user  ")]
    [TestMethod]
    public void Should_Trim_Whitespace_When_Create(string input_value)
    {
        const string actual_value = "user";

        Username.TryCreateNew(input_value, out var user);

        user.Value.ShouldBe(actual_value);
    }

    [TestMethod]
    public void Should_Set_Value_When_Create_From_Valid()
    {
        const string value = "user";

        Username.TryCreateNew(value, out var user);

        user.Value.ShouldBe(value);
    }

    [TestMethod]
    public void Should_Be_Equal_When_Same_Value()
    {
        const string value = "user";

        Username.TryCreateNew(value, out var user1);
        Username.TryCreateNew(value, out var user2);

        user1.ShouldBe(user2);
    }

    [DataRow("user  ")]
    [DataRow("   user")]
    [DataRow("   user  ")]
    [TestMethod]
    public void Should_Be_Equal_When_Same_Value_With_Whitespace(string value_with_whitespace)
    {
        const string value = "user";

        Username.TryCreateNew(value, out var user1);
        Username.TryCreateNew(value_with_whitespace, out var user2);

        user1.ShouldBe(user2);
    }

    [TestMethod]
    public void Should_Not_Be_Equal_When_Different_Value()
    {
        const string value1 = "quin";
        const string value2 = "ywwuyi";

        Username.TryCreateNew(value1, out var user1);
        Username.TryCreateNew(value2, out var user2);

        user1.ShouldNotBe(user2);
    }
}
