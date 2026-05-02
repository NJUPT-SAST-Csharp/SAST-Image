using Mediator;

namespace Domain.Extensions;

public sealed class UnitOfWorkPostProcessor<TCommand, TResponse>(IUnitOfWork unitOfWork)
    : MessagePostProcessor<TCommand, TResponse>
    where TCommand : IBaseCommand
{
    protected override async ValueTask Handle(
        TCommand message,
        TResponse response,
        CancellationToken cancellationToken
    )
    {
        await unitOfWork.CommitChangesAsync(cancellationToken);
    }
}
