using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.ImageEntity;
using Domain.CategoryAggregate.CategoryEntity;
using Domain.Database.Extensions;
using Domain.UserAggregate.IdentityEntity;
using Domain.UserAggregate.UserEntity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Database;

public sealed class DomainDbContext(DbContextOptions<DomainDbContext> options) : DbContext(options)
{
    public const string Schema = "domain";

    public DbSet<Album> Albums { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(Schema);

        DomainDbContextEntityTypeConfigurations configuration = new();
        modelBuilder.ApplyConfiguration<Album>(configuration);
        modelBuilder.ApplyConfiguration<Category>(configuration);
        modelBuilder.ApplyConfiguration<User>(configuration);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);

        builder.MapValueObject<AccessLevel, AccessLevelValue>();
        builder.MapValueObject<RefreshToken, string>();
        builder.MapValueObject<CategoryName, string>();
        builder.MapValueObject<IdentityId, long>();
        builder.MapValueObject<Username, string>();
        builder.MapValueObject<Email, string>();

        builder.MapValueObjects<Roles, Role>();
        //builder.MapValueObjects<ImageTags, string>();
        //builder.MapValueObjects<AlbumTags, string>();

        builder.MapTypedId<UserId>();
        builder.MapTypedId<ImageId>();
        builder.MapTypedId<AlbumId>();
        builder.MapTypedId<CategoryId>();
    }
}
