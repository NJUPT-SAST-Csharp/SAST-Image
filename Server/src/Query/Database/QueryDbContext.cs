using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Query.Albums;
using Query.Categories;
using Query.Images;
using Query.Users;

namespace Query.Database;

public sealed class QueryDbContext(DbContextOptions<QueryDbContext> options) : DbContext(options)
{
    public const string Schema = "query";

    public DbSet<AlbumModel> Albums { get; init; }
    public DbSet<ImageModel> Images { get; init; }
    public DbSet<UserModel> Users { get; init; }
    public DbSet<CategoryModel> Categories { get; init; }

    public DbSet<Checkpoint> Checkpoints { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Model.SetValueGenerationStrategy(
            Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.None
        );

        builder.HasDefaultSchema(Schema);

        QueryDbContextEntityTypeConfigurations configuration = new();

        builder.ApplyConfiguration<AlbumModel>(configuration);
        builder.ApplyConfiguration<UserModel>(configuration);
        builder.ApplyConfiguration<CategoryModel>(configuration);
        builder.ApplyConfiguration<ImageModel>(configuration);

        builder.Entity<Checkpoint>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Timestamp).IsUnique();
            entity.HasIndex(e => e.GrainId).IsUnique();
            entity.Property(e => e.Version).IsRowVersion();
        });
    }
}
