using Microsoft.EntityFrameworkCore;

namespace Shared.Core;

public abstract class DbContextWithCheckpoint<DatabaseContext>(
    DbContextOptions<DatabaseContext> options
) : DbContext(options)
    where DatabaseContext : DbContextWithCheckpoint<DatabaseContext>
{
    public required DbSet<Checkpoint> Checkpoints { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Model.SetValueGenerationStrategy(
            Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.None
        );

        OnModelCreatingCore(builder);

        builder.Entity<Checkpoint>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.HasIndex(e => e.Timestamp).IsUnique(false);
            entity.HasIndex(e => e.GrainId).IsUnique();
        });

        foreach (var type in builder.Model.GetEntityTypes())
        {
            builder.Entity(type.ClrType).Property<uint>("Version").IsRowVersion();
        }
    }

    protected abstract void OnModelCreatingCore(ModelBuilder builder);
}
