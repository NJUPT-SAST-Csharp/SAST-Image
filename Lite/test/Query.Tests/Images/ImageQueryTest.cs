using Query.Database;

namespace Query.Albums;

[TestClass]
public sealed class ImageQueryTest
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task Test()
    {
        await using var context = QueryDbContext.New;
    }
}
