using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Domain;

public sealed record Actor
{
    public UserId Id { get; init; }
    public bool IsAuthenticated { get; init; }
    public Role Role { get; init; }
    public bool IsAdmin => (Role & Role.Admin) == Role.Admin;

    public static implicit operator Actor(ClaimsPrincipal user) => new(user);
}

public static class ClaimsPrincipalExtensions
{
    extension(ClaimsPrincipal user)
    {
        public bool TryFetchClaim(string claim, [NotNullWhen(true)] out string? value)
        {
            var c = user.FindFirst(claim);
            value = c?.Value;
            return c is not null;
        }

        public bool TryFetchUsername([NotNullWhen(true)] out string? username)
        {
            return user.TryFetchClaim("username", out username);
        }

        public bool TryFetchId(out long id)
        {
            if (user.TryFetchClaim("id", out string? claim))
            {
                bool result = long.TryParse(claim, out id);
                return result;
            }
            id = 0;
            return false;
        }
    }
}

[Flags]
public enum Role : byte
{
    None = 0,
    User = 1,
    Admin = User << 1,
}
