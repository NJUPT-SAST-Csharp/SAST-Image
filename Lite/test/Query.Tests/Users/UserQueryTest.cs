using Query.Database;

namespace Query.Users;

[TestClass]
public sealed class UserQueryTest
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task Test()
    {
        await using var context = QueryDbContext.New;
    }
}
