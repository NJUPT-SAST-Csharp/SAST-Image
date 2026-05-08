namespace Domain.User;

public interface IUsernameUniquenessChecker
{
    public ValueTask<bool> ExistsAsync(
        Username username,
        CancellationToken cancellationToken = default
    );
}

[Alias("username_already_exists_exception")]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed class UsernameAlreadyExistsException(Username username)
    : Exception($"Username '{username.Value}' already exists.")
{
    public Username Username { get; } = username;
}
