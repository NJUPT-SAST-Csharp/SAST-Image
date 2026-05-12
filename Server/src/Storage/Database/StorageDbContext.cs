using Domain.Event;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Storage.Database;

public sealed class StorageDbContext(DbContextOptions<StorageDbContext> options)
    : DbContext(options)
{
    const string Schema = "storage";

    public DbSet<Checkpoint> Checkpoints { get; init; }
    public DbSet<AccessControl> AccessControlList { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Model.SetValueGenerationStrategy(
            Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.None
        );

        builder.HasDefaultSchema(Schema);

        builder.Entity<Checkpoint>(cp =>
        {
            cp.HasKey(c => c.Id);
            cp.Property(c => c.Id).ValueGeneratedNever();
            cp.HasIndex(c => c.Timestamp).IsUnique(false);
            cp.HasIndex(c => c.GrainId).IsUnique();
            cp.Property(c => c.Version).IsRowVersion();
        });

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
