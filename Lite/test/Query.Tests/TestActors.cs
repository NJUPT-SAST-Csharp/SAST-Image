using System.Security.Claims;
using Domain.Shared;
using Domain.UserAggregate.UserEntity;

namespace Query.Tests;

internal static class TestActors
{
    public static Actor Anonymous => new();

    public static Actor Authenticated(long userId) =>
        new(new ClaimsPrincipal(new ClaimsIdentity([new Claim("id", userId.ToString())])));

    public static Actor Admin(long userId) =>
        new(
            new ClaimsPrincipal(
                new ClaimsIdentity(
                    [new Claim("id", userId.ToString()), new Claim("role", Role.Admin.ToString())]
                )
            )
        );
}
