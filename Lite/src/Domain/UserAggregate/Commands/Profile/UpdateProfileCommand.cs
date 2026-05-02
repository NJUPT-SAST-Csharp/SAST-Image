using Domain.Shared;
using Domain.UserAggregate.UserEntity;
using Mediator;

namespace Domain.UserAggregate.Commands.Profile;

public sealed record class UpdateProfileCommand(
    Nickname? Nickname,
    Biography? Biography,
    Actor Actor
) : ICommand;

internal sealed class UpdateProfileCommandHandler(IUserRepository repository)
    : ICommandHandler<UpdateProfileCommand>
{
    public async ValueTask<Unit> Handle(
        UpdateProfileCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await repository.GetAsync(command.Actor.Id, cancellationToken);

        user.UpdateProfile(command);

        return Unit.Value;
    }
}
