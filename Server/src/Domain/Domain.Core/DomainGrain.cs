using Domain.Database;
using Domain.Event;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orleans.EventSourcing;
using Orleans.EventSourcing.CustomStorage;

namespace Domain;

public abstract class DomainGrain<TState> : DomainGrain<TState, DomainEventBase>
    where TState : DomainStateBase, IDomainEventApplyable<DomainEventBase>, new();

public abstract class DomainGrain<TState, TEventBase>
    : JournaledGrain<TState, TEventBase>,
        ICustomStorageInterface<TState, TEventBase>,
        IDomainGrainStateRecordExistenceIndicator
    where TState : DomainStateBase, IDomainEventApplyable<TEventBase>, new()
    where TEventBase : DomainEventBase
{
    public bool RecordExists => State.RecordExists;

    protected long Id => this.GetPrimaryKeyLong();
    protected Actor Actor =>
        RequestContext.Get(nameof(Actor)) as Actor ?? throw new ForbiddenException();

    public async Task<bool> ApplyUpdatesToStorage(
        IReadOnlyList<TEventBase> updates,
        int expectedVersion
    )
    {
        if (updates.Count == 0)
            return true;

        var grainId = this.GetPrimaryKeyLong();

        await using var context = await ServiceProvider
            .GetRequiredService<IDbContextFactory<DomainDbContext>>()
            .CreateDbContextAsync();

        var currentVersion = await context
            .Events.AsNoTracking()
            .Where(e => e.GrainId == grainId)
            .Select(e => e.ETag)
            .OrderByDescending(e => e)
            .FirstOrDefaultAsync();

        if (currentVersion != expectedVersion)
            return false;

        List<DomainEventUnit> events = [];

        foreach (var e in updates)
        {
            currentVersion++;
            events.Add(
                new DomainEventUnit
                {
                    Value = e,
                    GrainId = grainId,
                    ETag = currentVersion,
                    Timestamp = e.Timestamp,
                }
            );
        }

        await context.Events.AddRangeAsync(events);
        await context.SaveChangesAsync();

        var snapshot = await context.Snapshots.SingleOrDefaultAsync(s => s.Id == grainId);

        var state = snapshot?.Value as TState ?? new();

        foreach (var e in updates)
            state.Apply(e);

        if (snapshot is null)
        {
            snapshot = new()
            {
                ETag = currentVersion,
                Id = grainId,
                Value = state,
            };
            await context.Snapshots.AddAsync(snapshot);
        }
        else
        {
            snapshot.ETag = currentVersion;
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<KeyValuePair<int, TState>> ReadStateFromStorage()
    {
        var grainId = this.GetPrimaryKeyLong();

        await using var context = await ServiceProvider
            .GetRequiredService<IDbContextFactory<DomainDbContext>>()
            .CreateDbContextAsync();
        var snapshot = await context
            .Snapshots.AsNoTracking()
            .SingleOrDefaultAsync(s => s.Id == grainId);

        var state = snapshot?.Value as TState ?? new();
        var version = snapshot?.ETag ?? 0;
        var events = await context
            .Events.AsNoTracking()
            .Where(e => e.GrainId == grainId && e.ETag > version)
            .OrderBy(e => e.ETag)
            .Select(e => e.Value)
            .ToArrayAsync();

        foreach (var e in events)
        {
            state.Apply((TEventBase)e);
            version++;
        }

        return new(version, state);
    }
}

public interface IDomainEventApplyable : IDomainEventApplyable<DomainEventBase>;

public interface IDomainEventApplyable<TEventBase>
    where TEventBase : DomainEventBase
{
    void Apply(TEventBase e);
}
