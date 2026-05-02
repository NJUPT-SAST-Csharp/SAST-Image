using Domain.UserAggregate.IdentityEntity;
using Domain.UserAggregate.Services;
using Domain.UserAggregate.UserEntity;
using Mediator;

namespace Domain.UserAggregate.Commands.OAuth.GitHub;

public sealed record class GitHubLinkCommand(UserId UserId, IdentityId GitHubId) : ICommand;

internal sealed class GitHubLinkCommandHandler(
    IUserRepository repository,
    IIdentityUniquenessChecker checker
) : ICommandHandler<GitHubLinkCommand>
{
    public async ValueTask<Unit> Handle(
        GitHubLinkCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await repository.GetAsync(command.UserId, cancellationToken);

        await user.LinkAsync(command, checker, cancellationToken);

        return Unit.Value;
    }
}
