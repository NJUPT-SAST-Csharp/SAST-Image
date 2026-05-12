using Domain.Event;
using Microsoft.EntityFrameworkCore;

namespace Domain.Database;

public sealed class DomainDbContext(DbContextOptions<DomainDbContext> options) : DbContext(options)
{
    public const string Scheme = "domain";
    public required DbSet<DomainStateUnit> Snapshots { get; init; }
    public required DbSet<DomainEventUnit> Events { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Model.SetValueGenerationStrategy(
            Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.None
        );

        builder.HasDefaultSchema(Scheme);

        var snapshots = builder.Entity<DomainStateUnit>();
        snapshots.HasKey(s => s.Id);
        snapshots.HasIndex(s => s.ETag);
        snapshots.Property(s => s.Value).HasColumnType("jsonb");

        var events = builder.Entity<DomainEventUnit>();
        events.HasKey(e => e.EventId);
        events.HasIndex(e => new { e.GrainId, e.ETag }).IsUnique();
        events.HasIndex(e => e.Timestamp);
        events.Property(e => e.Value).HasColumnType("jsonb");
    }
}
