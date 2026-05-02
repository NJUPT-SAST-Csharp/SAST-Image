using Domain;
using Domain.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

namespace Infrastructure;

[TestClass]
public static class App
{
    public static readonly DbContextOptions<DomainDbContext> Options =
        new DbContextOptionsBuilder<DomainDbContext>()
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSnakeCaseNamingConvention()
            .UseNpgsql(
                new NpgsqlConnection(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")),
                options => options.MigrationsAssembly(DomainAssembly.Assembly)
            )
            .Options;

    [AssemblyInitialize]
    public static async Task Initialize(TestContext tc)
    {
        await using DomainDbContext context = new(Options);
        await context.Database.EnsureCreatedAsync(tc.CancellationToken);
        await context.Database.ExecuteSqlRawAsync(
            $"DROP SCHEMA IF EXISTS {DomainDbContext.Schema} CASCADE",
            tc.CancellationToken
        );
        await context
            .Database.GetService<IRelationalDatabaseCreator>()
            .CreateTablesAsync(tc.CancellationToken);
    }

    [AssemblyCleanup]
    public static async Task Cleanup(TestContext tc)
    {
        await using DomainDbContext context = new(Options);
        //await context.Database.ExecuteSqlRawAsync(
        //    $"DROP SCHEMA IF EXISTS {DomainDbContext.Schema} CASCADE",
        //    tc.CancellationToken
        //);
    }
}
