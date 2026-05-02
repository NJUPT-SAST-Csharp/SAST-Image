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
    public DbSet<LikeModel> Likes { get; init; }
    public DbSet<SubscribeModel> Subscribes { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(Schema);

        QueryDbContextEntityTypeConfigurations configuration = new();

        modelBuilder.ApplyConfiguration<AlbumModel>(configuration);
        modelBuilder.ApplyConfiguration<UserModel>(configuration);
        modelBuilder.ApplyConfiguration<CategoryModel>(configuration);
        modelBuilder.ApplyConfiguration<ImageModel>(configuration);
        modelBuilder.ApplyConfiguration<LikeModel>(configuration);
        modelBuilder.ApplyConfiguration<SubscribeModel>(configuration);
    }
}
