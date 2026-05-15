using Microsoft.EntityFrameworkCore;
using Query.Album;
using Query.Category;
using Query.Image;
using Query.User;
using Shared.Core;

namespace Query.Database;

public sealed class QueryDbContext(DbContextOptions<QueryDbContext> options)
    : DbContextWithCheckpoint<QueryDbContext>(options)
{
    public const string Schema = "query";

    public DbSet<AlbumModel> Albums { get; init; }
    public DbSet<ImageModel> Images { get; init; }
    public DbSet<UserModel> Users { get; init; }
    public DbSet<CategoryModel> Categories { get; init; }

    protected override void OnModelCreatingCore(ModelBuilder builder)
    {
        if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
        {
            builder.Model.SetValueGenerationStrategy(
                Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.None
            );
        }

        builder.HasDefaultSchema(Schema);

        QueryDbContextEntityTypeConfigurations configuration = new();

        builder.ApplyConfiguration<AlbumModel>(configuration);
        builder.ApplyConfiguration<UserModel>(configuration);
        builder.ApplyConfiguration<CategoryModel>(configuration);
        builder.ApplyConfiguration<ImageModel>(configuration);
    }
}
