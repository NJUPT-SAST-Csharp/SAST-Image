using Query.Database;

namespace Query.Categories;

[TestClass]
public sealed class CategoryQueryTest
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task Test()
    {
        await using var context = QueryDbContext.New;
    }
}
