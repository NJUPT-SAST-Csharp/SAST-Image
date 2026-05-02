using Domain.Shared;
using Mediator;

namespace Domain.UserAggregate.Commands.Profile;

public sealed record UpdateHeaderCommand(ImageFile Header, Actor Actor) : ICommand { }

internal sealed class UpdateHeaderCommandHandler(IUserRepository repository)
    : ICommandHandler<UpdateHeaderCommand>
{
    public async ValueTask<Unit> Handle(
        UpdateHeaderCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await repository.GetAsync(command.Actor.Id, cancellationToken);

        user.UpdateHeader(command);

        return Unit.Value;
    }
}
