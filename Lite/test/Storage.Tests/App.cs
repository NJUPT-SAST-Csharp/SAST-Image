using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using Storage.Database;

namespace Storage;

[TestClass]
public static class App
{
    public static readonly DbContextOptions<StorageDbContext> Options =
        new DbContextOptionsBuilder<StorageDbContext>()
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSnakeCaseNamingConvention()
            .UseNpgsql(
                new NpgsqlConnection(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")),
                options => options.MigrationsAssembly(StorageAssembly.Assembly)
            )
            .Options;

    [AssemblyInitialize]
    public static async Task Initialize(TestContext tc)
    {
        await using StorageDbContext context = new(Options);
        await context.Database.EnsureCreatedAsync(tc.CancellationToken);
        await context.Database.ExecuteSqlRawAsync(
            $"DROP SCHEMA IF EXISTS {StorageDbContext.Schema} CASCADE",
            tc.CancellationToken
        );
        await context
            .Database.GetService<IRelationalDatabaseCreator>()
            .CreateTablesAsync(tc.CancellationToken);
    }

    [AssemblyCleanup]
    public static async Task Cleanup(TestContext tc)
    {
        await using StorageDbContext context = new(Options);
        //await context.Database.ExecuteSqlRawAsync(
        //    $"DROP SCHEMA IF EXISTS {StorageDbContext.Schema} CASCADE",
        //    tc.CancellationToken
        //);
    }
}
