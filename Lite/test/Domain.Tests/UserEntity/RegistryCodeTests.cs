using Domain.UserAggregate.UserEntity;
using Shouldly;

namespace Domain.Tests.UserEntity;

[TestClass]
public sealed class RegistryCodeTests
{
    private static readonly IEnumerable<object[]> valid_values =
    [
        [RegistryCode.MinValue],
        [RegistryCode.MaxValue],
        [RegistryCode.MinValue + 1],
        [RegistryCode.MaxValue - 1],
    ];

    [TestMethod]
    [DynamicData(nameof(valid_values))]
    public void Return_True_When_Create_From_Valid_Value(int value)
    {
        bool result = RegistryCode.TryCreateNew(value, out var _);

        result.ShouldBeTrue();
    }

    [TestMethod]
    [DynamicData(nameof(valid_values))]
    public void Should_Set_Value_When_Create_From_Valid_Value(int value)
    {
        RegistryCode.TryCreateNew(value, out var registry_code);

        registry_code.Value.ShouldBe(value);
    }

    private static readonly IEnumerable<object[]> invalid_values =
    [
        [RegistryCode.MinValue - 1],
        [RegistryCode.MaxValue + 1],
        [int.MaxValue],
        [int.MinValue],
        [0],
    ];

    [TestMethod]
    [DynamicData(nameof(invalid_values))]
    public void Return_False_When_Create_From_Invalid_Value(int value)
    {
        bool result = RegistryCode.TryCreateNew(value, out var _);

        result.ShouldBeFalse();
    }

    [TestMethod]
    public void Should_Be_Equal_When_Same_Value()
    {
        int value = (int)valid_values.ToArray()[0][0];

        RegistryCode.TryCreateNew(value, out var registry_code1);
        RegistryCode.TryCreateNew(value, out var registry_code2);

        registry_code1.ShouldBe(registry_code2);
    }

    [TestMethod]
    public void Should_Not_Be_Equal_When_Different_Value()
    {
        int value1 = (int)valid_values.ToArray()[0][0];
        int value2 = (int)valid_values.ToArray()[1][0];

        RegistryCode.TryCreateNew(value1, out var registry_code1);
        RegistryCode.TryCreateNew(value2, out var registry_code2);

        registry_code1.ShouldNotBe(registry_code2);
    }
}
