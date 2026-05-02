using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using Query.Albums;
using Query.Categories;
using Query.Database;
using Query.Images;
using Query.Users;

namespace Query;

[TestClass]
public static class App
{
    public static readonly DbContextOptions<QueryDbContext> Options =
        new DbContextOptionsBuilder<QueryDbContext>()
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSnakeCaseNamingConvention()
            .UseNpgsql(
                new NpgsqlConnection(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")),
                options => options.MigrationsAssembly(QueryAssembly.Assembly)
            )
            .UseModel(QueryDbContextModel.Instance)
            .Options;

    [AssemblyInitialize]
    public static async Task Initialize(TestContext tc)
    {
        await using QueryDbContext context = new(Options);

        await context.Database.EnsureCreatedAsync(tc.CancellationToken);
        await context.Database.ExecuteSqlRawAsync(
            $"DROP SCHEMA IF EXISTS {QueryDbContext.Schema} CASCADE",
            tc.CancellationToken
        );
        await context
            .Database.GetService<IRelationalDatabaseCreator>()
            .CreateTablesAsync(tc.CancellationToken);

        await context.Categories.AddRangeAsync(CategorySeedData.Seed, tc.CancellationToken);
        await context.Albums.AddRangeAsync(AlbumSeedData.Seed, tc.CancellationToken);
        await context.Images.AddRangeAsync(ImageSeedData.Seed, tc.CancellationToken);
        await context.Users.AddRangeAsync(UserSeedData.Seed, tc.CancellationToken);

        await context.SaveChangesAsync(tc.CancellationToken);
    }

    [AssemblyCleanup]
    public static async Task Cleanup(TestContext tc)
    {
        await using QueryDbContext context = new(Options);
        //await context.Database.ExecuteSqlRawAsync(
        //    $"DROP SCHEMA IF EXISTS {QueryDbContext.Schema} CASCADE",
        //    tc.CancellationToken
        //);
    }
}

internal static class AppExtensions
{
    extension(QueryDbContext context)
    {
        public static QueryDbContext New => new(App.Options);
    }
}
