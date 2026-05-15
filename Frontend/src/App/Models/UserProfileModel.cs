using Domain.Api;

namespace App.Models;

public sealed class UserProfileModel
{
    public long Id { get; init; }
    public string Username { get; init; } = "";
    public string Nickname { get; init; } = "";
    public string Biography { get; init; } = "";
    public long RegisteredAt { get; init; }

    public static implicit operator UserProfileModel(UserProfileResponse r) => new()
    {
        Id = r.Id,
        Username = r.Username,
        Nickname = r.Nickname,
        Biography = r.Biography,
        RegisteredAt = r.RegisteredAt,
    };
}
