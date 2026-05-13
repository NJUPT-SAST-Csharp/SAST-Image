using Domain.Event;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Shared.Core;

public abstract partial class EventSyncBackgroundService<TService, TDbContext>(
    IServiceScopeFactory factory,
    ILogger<TService> logger
) : EventSyncBackgroundService<TService>(factory, logger)
    where TService : EventSyncBackgroundService<TService, TDbContext>
    where TDbContext : DbContextWithCheckpoint<TDbContext>
{
    private readonly HashSet<string> missingHandlerTypes = [];
    private IEventStoreGrain? grain = null;

    protected override async Task Sync(
        IServiceProvider services,
        CancellationToken cancellationToken
    )
    {
        await using var context = services.GetRequiredService<TDbContext>();
        var mediator = services.GetRequiredService<IMediator>();
        var points = await context.GetCheckpointsAsync(cancellationToken);

        var cursor = points.First(p => p.Id == Checkpoint.CursorId);
        var failedPoints = points
            .Where(p => p.Status == CheckpointStatus.Failed)
            .Select(p => p.Id)
            .ToHashSet();

        grain ??= services
            .GetRequiredService<IGrainFactory>()
            .GetGrain<IEventStoreGrain>(Guid.Empty);

        var events = await grain.GetEventsAsync(cursor.Timestamp, cancellationToken);

        foreach (var e in events)
        {
            try
            {
                if (failedPoints.Contains(e.EventId) || missingHandlerTypes.Contains(e.Type))
                    continue;
                await mediator.Send(e.Value, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex) when (ex.IsDueToDuplicate)
            {
                //Silence and skip
                continue;
            }
            catch (OperationCanceledException)
            {
                //Silence and skip
                continue;
            }
            catch (MissingMessageHandlerException)
            {
                var type = e.Type;
                missingHandlerTypes.Add(type);
                LogMissingHandler(Logger, type);
            }
            catch (Exception ex)
            {
                LogFailedEvent(Logger, ex, e.EventId);
                failedPoints.Add(e.EventId);
                await context.RecordFailedPointAsync(e, cancellationToken);
            }
            finally
            {
                await context.SaveCursorAsync(e, cancellationToken);
            }
        }
    }

    [LoggerMessage(LogLevel.Warning, "Unable to process outbox event {EventId}")]
    static partial void LogFailedEvent(ILogger<TService> logger, Exception exception, Guid eventId);

    [LoggerMessage(LogLevel.Warning, "Missing handler for outbox event {EventType}")]
    static partial void LogMissingHandler(ILogger<TService> logger, string eventType);
}

public abstract partial class EventSyncBackgroundService<TService>(
    IServiceScopeFactory factory,
    ILogger<TService> logger
) : BackgroundService
    where TService : EventSyncBackgroundService<TService>
{
    protected ILogger<TService> Logger { get; } = logger;
    protected virtual int IntervalSeconds => 1;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(IntervalSeconds));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await using var scope = factory.CreateAsyncScope();

            try
            {
                await Sync(scope.ServiceProvider, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                //Silence and skip
                continue;
            }
            catch (Exception ex)
            {
                LogFailedSync(Logger, ex);
            }
        }
    }

    protected abstract Task Sync(IServiceProvider services, CancellationToken cancellationToken);

    [LoggerMessage(LogLevel.Error, "Failed to sync temporarily")]
    static partial void LogFailedSync(ILogger<TService> logger, Exception ex);
}

file static class DbContextExtensions
{
    extension<TDbContext>(TDbContext context)
        where TDbContext : DbContextWithCheckpoint<TDbContext>
    {
        public async Task<Checkpoint[]> GetCheckpointsAsync(CancellationToken cancellationToken)
        {
            var points = await context.Checkpoints.AsNoTracking().ToArrayAsync(cancellationToken);
            var cursor = points.FirstOrDefault(cp => cp.Id == Checkpoint.CursorId);

            if (points.Length <= 0 || cursor is null)
            {
                try
                {
                    cursor = new Checkpoint
                    {
                        Id = Checkpoint.CursorId,
                        Timestamp = DateTime.MinValue,
                    };
                    points = [.. points, cursor];
                    await context.Checkpoints.AddAsync(cursor, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    context.ChangeTracker.Clear();
                    points = await context
                        .Checkpoints.AsNoTracking()
                        .ToArrayAsync(cancellationToken);
                }
                catch (DbUpdateException ex) when (ex.IsDueToDuplicate)
                {
                    context.ChangeTracker.Clear();
                    points = await context
                        .Checkpoints.AsNoTracking()
                        .ToArrayAsync(cancellationToken);
                }
            }

            return points;
        }

        public async Task SaveCursorAsync(
            DomainEventUnit currentEvent,
            CancellationToken cancellationToken
        )
        {
            try
            {
                await context
                    .Checkpoints.Where(cp => cp.Id == Checkpoint.CursorId)
                    .ExecuteUpdateAsync(
                        cp => cp.SetProperty(p => p.Timestamp, currentEvent.Timestamp),
                        cancellationToken
                    );
            }
            catch (DbUpdateConcurrencyException)
            {
                //Silence and skip
                return;
            }
        }

        public async Task RecordFailedPointAsync(
            DomainEventUnit e,
            CancellationToken cancellationToken
        )
        {
            try
            {
                context.ChangeTracker.Clear();
                Checkpoint failed = new()
                {
                    Id = e.EventId,
                    Timestamp = e.Timestamp,
                    GrainId = e.GrainId,
                    Status = CheckpointStatus.Failed,
                };
                await context.Checkpoints.AddAsync(failed, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Silence and skip
                return;
            }
            catch (DbUpdateException ex) when (ex.IsDueToDuplicate)
            {
                //Silence and skip
                return;
            }
        }
    }

    extension(DbUpdateException ex)
    {
        // NOTE: fragile check
        public bool IsDueToDuplicate =>
            ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation };
    }
}
