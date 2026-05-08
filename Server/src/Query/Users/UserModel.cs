global using UserId = long;
using Domain.User.Events;

namespace Query.Users;

public sealed class UserModel
{
    [Obsolete("For ORM", true)]
    private UserModel() { }

    public long Id { get; }
    public string Username { get; internal set; } = null!;
    public string Nickname { get; internal set; } = null!;
    public string Biography { get; internal set; } = string.Empty;
    public DateTime RegisteredAt { get; init; }

    internal UserModel(UserRegisteredEvent e)
    {
        Id = e.Id.Value;
        Username = e.Username.Value;
        Nickname = e.Nickname.Value;
        RegisteredAt = DateTime.UtcNow;
    }
}
