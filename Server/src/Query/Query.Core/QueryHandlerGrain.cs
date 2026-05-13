using Mediator;
using Orleans.Concurrency;

namespace Query;

[StatelessWorker]
internal sealed class QueryHandlerGrain(IMediator mediator) : Grain, IQueryHandlerGrain
{
    public async Task<TResult> QueryAsync<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken
    )
    {
        return await mediator.Send(query, cancellationToken);
    }
}
