using Domain.UserAggregate.Services;
using Domain.UserAggregate.UserEntity;
using Mediator;

namespace Domain.UserAggregate.Commands;

public sealed record SendRegistryCodeCommand(Email Email) : ICommand { }

internal sealed class SendRegistryCodeCommandHandler(IRegistryCodeEmailClient client)
    : ICommandHandler<SendRegistryCodeCommand>
{
    public async ValueTask<Unit> Handle(
        SendRegistryCodeCommand command,
        CancellationToken cancellationToken
    )
    {
        var code = RegistryCode.GenerateNew();

        await client.SendAsync(command.Email, code, cancellationToken);

        return Unit.Value;
    }
}
