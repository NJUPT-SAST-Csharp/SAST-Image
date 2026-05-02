using Domain.UserAggregate.UserEntity;
using Shouldly;

namespace Domain.Tests.UserEntity;

[TestClass]
public sealed class EmailTests
{
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow("bruh")]
    [DataRow("bruh@")]
    [DataRow("@bruh.com")]
    [DataRow("bruh@@example.com")]
    [TestMethod]
    public void Return_False_When_Create_From_Invalid(string value)
    {
        bool result = Email.TryCreateNew(value, out var _);

        result.ShouldBeFalse();
    }

    [DataRow("bruh@example")] // NOTE: This is technically valid according to RFC 5322.
    [DataRow("bruh@example.com")]
    [DataRow("bruh+tag@example.com")]
    [DataRow("first.last@example.co.uk")]
    [TestMethod]
    public void Return_True_When_Create_From_Valid(string value)
    {
        bool result = Email.TryCreateNew(value, out var _);

        result.ShouldBeTrue();
    }

    [DataRow("bruh@example.com  ")]
    [DataRow("  bruh@example.com")]
    [DataRow("  bruh@example.com  ")]
    [TestMethod]
    public void Should_Trim_Whitespace_When_Create(string input_value)
    {
        const string actual_value = "bruh@example.com";

        Email.TryCreateNew(input_value, out var email);

        email.Value.ShouldBe(actual_value);
    }

    [TestMethod]
    public void Should_Set_Value_When_Create_From_Valid()
    {
        const string value = "bruh@example.com";

        Email.TryCreateNew(value, out var email);

        email.Value.ShouldBe(value);
    }

    [TestMethod]
    public void Should_Be_Equal_When_Same_Value()
    {
        const string value = "bruh@example.com";

        Email.TryCreateNew(value, out var email1);
        Email.TryCreateNew(value, out var email2);

        email1.ShouldBe(email2);
    }

    [DataRow("bruh@example.com  ")]
    [DataRow("  bruh@example.com")]
    [DataRow("  bruh@example.com  ")]
    [TestMethod]
    public void Should_Be_Equal_When_Same_Value_With_Whitespace(string value_with_whitespace)
    {
        const string value = "bruh@example.com";

        Email.TryCreateNew(value, out var email1);
        Email.TryCreateNew(value_with_whitespace, out var email2);

        email1.ShouldBe(email2);
    }

    [TestMethod]
    public void Should_Not_Be_Equal_When_Different_Value()
    {
        const string value1 = "bruh1@example.com";
        const string value2 = "bruh2@example.com";

        Email.TryCreateNew(value1, out var email1);
        Email.TryCreateNew(value2, out var email2);

        email1.ShouldNotBe(email2);
    }
}
