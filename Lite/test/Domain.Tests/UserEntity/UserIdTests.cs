using System.Text.Json;
using Domain.UserAggregate.UserEntity;
using Shouldly;

namespace Domain.Tests.ImageEntity;

[TestClass]
public sealed class UserIdTests
{
    [TestMethod]
    public void Convert_From_Json()
    {
        var id = UserId.New;

        var converted = JsonSerializer.Deserialize<UserId>(id.Value);

        id.ShouldBe(converted);
    }
}

internal static class TestUserId
{
    extension(UserId)
    {
        public static UserId New => UserId.GenerateNew();
    }
}
