using Mediator;

namespace Query;

[Alias("QueryGrain")]
public interface IQueryHandlerGrain : IGrainWithGuidKey
{
    [Alias(nameof(QueryAsync))]
    public Task<TResult> QueryAsync<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken
    );
}
