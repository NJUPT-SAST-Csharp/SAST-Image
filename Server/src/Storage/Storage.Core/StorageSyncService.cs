using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Core;
using Storage.Database;

namespace Storage;

public sealed class StorageSyncService(IServiceScopeFactory factory, ILogger<StorageSyncService> logger)
    : EventSyncBackgroundService<StorageSyncService, StorageDbContext>(factory, logger);
