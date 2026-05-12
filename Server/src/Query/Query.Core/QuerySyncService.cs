using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Query.Database;
using Shared.Core;

namespace Query;

public sealed class QuerySyncService(IServiceScopeFactory factory, ILogger<QuerySyncService> logger)
    : EventSyncBackgroundService<QuerySyncService, QueryDbContext>(factory, logger);
