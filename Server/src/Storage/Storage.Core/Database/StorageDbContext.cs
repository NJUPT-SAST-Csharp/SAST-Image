using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Core;

namespace Storage.Database;

public sealed class StorageDbContext(DbContextOptions<StorageDbContext> options)
    : DbContextWithCheckpoint<StorageDbContext>(options)
{
    const string Schema = "storage";

    public DbSet<AccessControl> AccessControlList { get; set; }

    protected override void OnModelCreatingCore(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schema);

        builder.Entity<AccessControl>(ac =>
        {
            ac.HasKey(a => a.ResourceId);
            ac.PrimitiveCollection(a => a.Users)
                .ElementType(e =>
                    e.HasConversion(
                        new ValueConverter<UserId, long>(id => id.Value, value => new(value)),
                        new ValueComparer<UserId>((id1, id2) => id1 == id2, id => id.GetHashCode())
                    )
                );
        });
    }
}
