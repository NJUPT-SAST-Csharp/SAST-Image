using Domain.UserAggregate.IdentityEntity;
using Domain.UserAggregate.Services;
using Domain.UserAggregate.UserEntity;
using Mediator;

namespace Domain.UserAggregate.Commands.OAuth.GitHub;

/// <summary>
/// Represents a command to initiate a login process using a GitHub external identifier.
/// </summary>
/// <remarks>
/// This command will return null when user corresponding to the id doesn't exist.
/// </remarks>
/// <param name="GitHubLogin">The external identifier associated with the user's GitHub account. Cannot be null.</param>
public sealed record GitHubLoginCommand(IdentityId GitHubLogin) : ICommand<JwtToken?>;

internal sealed class GitHubLoginCommandHandler(
    IUserRepository repository,
    IJwtTokenGenerator generator
) : ICommandHandler<GitHubLoginCommand, JwtToken?>
{
    public async ValueTask<JwtToken?> Handle(
        GitHubLoginCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await repository.GetOrDefaultAsync(command.GitHubLogin, cancellationToken);

        if (user is null)
            return null;

        return user.Login(command, generator);
    }
}
