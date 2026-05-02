using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Domain.UserAggregate.UserEntity;

namespace Domain.Shared;

public readonly record struct Actor
{
    public readonly UserId Id { get; init; }
    public readonly bool IsAuthenticated { get; init; }
    public readonly bool IsAdmin { get; init; }

    public Actor(ClaimsPrincipal user)
    {
        IsAuthenticated = user.TryFetchId(out long id);
        Id = new(id);
        if (IsAuthenticated)
        {
            IsAdmin = user.HasRole(Role.Admin);
        }
    }

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

        public bool HasRole(Role role)
        {
            foreach (var r in user.FindAll("role"))
            {
                if (
                    r is { } roleClaim
                    && string.Equals(
                        roleClaim.Value,
                        role.ToString(),
                        StringComparison.InvariantCultureIgnoreCase
                    )
                )
                    return true;
            }
            return false;
        }
    }
}
