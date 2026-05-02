using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Storage.Database;
using static Storage.OutboxMessage;

namespace Storage;

internal sealed partial class StorageService(
    IServiceScopeFactory scopeFactory,
    ILogger<StorageService> logger
) : BackgroundService
{
    const int intervalSeconds = 1;
    private static readonly int[] retryIntervals = [0, 1, 2, 4, 8, 16, 32];

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(intervalSeconds));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await using var scope = scopeFactory.CreateAsyncScope();
                await using var context =
                    scope.ServiceProvider.GetRequiredService<StorageDbContext>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var now = DateTime.UtcNow;
                var messages = await context
                    .Messages.Where(m => m.Status == MessageStatus.Staging)
                    .Where(m => m.RetryAt == null || m.RetryAt <= now)
                    .OrderBy(m => m.Time)
                    .ToArrayAsync(stoppingToken);

                foreach (var message in messages)
                {
                    try
                    {
                        LogProcessingMessage(logger, message.Id, message.RetryCount);
                        message.Status = MessageStatus.Pending;
                        await mediator.Send(message, stoppingToken);
                        context.Messages.Remove(message);
                    }
                    catch (Exception ex)
                    {
                        message.RetryCount++;
                        message.Status = MessageStatus.Staging;

                        if (message.RetryCount >= retryIntervals.Length)
                        {
                            LogFailedMessage(logger, ex, message.Id);
                            message.Status = MessageStatus.Failed;
                            continue;
                        }

                        message.RetryAt =
                            DateTime.UtcNow
                            + TimeSpan.FromSeconds(
                                Random.Shared.Next(0, retryIntervals[message.RetryCount])
                            );
                    }
                }

                await context.SaveChangesAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                LogServiceDown(logger, ex);
            }
        }
    }

    [LoggerMessage(LogLevel.Error, "Storage service down temporarily")]
    static partial void LogServiceDown(ILogger<StorageService> logger, Exception exception);

    [LoggerMessage(
        LogLevel.Information,
        "Processing outbox message {MessageId}, attempt {RetryCount}"
    )]
    static partial void LogProcessingMessage(
        ILogger<StorageService> logger,
        Guid messageId,
        int retryCount
    );

    [LoggerMessage(LogLevel.Warning, "Failed to process outbox message {MessageId}")]
    static partial void LogFailedMessage(
        ILogger<StorageService> logger,
        Exception exception,
        Guid messageId
    );
}
