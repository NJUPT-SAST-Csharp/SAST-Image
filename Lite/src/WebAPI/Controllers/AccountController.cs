using System.ComponentModel.DataAnnotations;
using AspNet.Security.OAuth.GitHub;
using Domain.Shared;
using Domain.UserAggregate.Commands;
using Domain.UserAggregate.Commands.OAuth.GitHub;
using Domain.UserAggregate.UserEntity;
using Infrastructure;
using Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Query.Users.Queries;
using WebAPI.Utilities;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Controllers;

[Route("api/v1/account")]
[ApiController]
public sealed class AccountController(IMediator mediator) : AdvancedController
{
    public readonly record struct RegisterRequest(
        [property: Required] Username Username,
        [property: Required] Nickname Nickname,
        [property: Required] Email Email,
        [property: Required] PasswordInput Password,
        [property: Required] RegistryCode Code
    );

    [HttpPost("register")]
    [EndpointName("Register")]
    [EndpointDescription(
        "Register a new user account with the provided information and registry code."
    )]
    [MaybeConflict]
    public async Task<Ok<JwtToken>> Register(
        [FromBody, Required] RegisterRequest request,
        CancellationToken cancellationToken
    )
    {
        RegisterCommand command = new(
            request.Username,
            request.Nickname,
            request.Email,
            request.Password,
            request.Code
        );
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    public readonly record struct SendRegistryCodeRequest([property: Required] Email Email);

    [HttpPost("register/code")]
    [EndpointName("Send Registry Code")]
    [EndpointDescription("Send a registry code to the specified email address.")]
    public async Task<NoContent> SendRegistryCode(
        [FromBody, Required] SendRegistryCodeRequest request,
        CancellationToken cancellationToken
    )
    {
        SendRegistryCodeCommand command = new(request.Email);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    public readonly record struct LoginRequest(
        [property: Required] Username Username,
        [property: Required] PasswordInput Password
    );

    [HttpPost("login")]
    [EndpointName("Login")]
    [EndpointDescription("Authenticate a user and return a JWT token.")]
    public async Task<Ok<JwtToken>> Login(
        [FromBody, Required] LoginRequest request,
        CancellationToken cancellationToken
    )
    {
        LoginCommand command = new(request.Username, request.Password);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    public readonly record struct RefreshTokenRequest(
        [property: Required] RefreshToken RefreshToken
    );

    [HttpPost("refresh")]
    [EndpointName("Refresh Access Token")]
    [EndpointDescription("Refresh the access token using a valid refresh token.")]
    [MaybeNotFound]
    public async Task<Ok<JwtToken>> RefreshAccessToken(
        [FromBody, Required] RefreshTokenRequest request,
        CancellationToken cancellationToken
    )
    {
        RefreshTokenCommand command = new(request.RefreshToken);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    public readonly record struct ResetPasswordRequest(
        [property: Required] PasswordInput OldPassword,
        [property: Required] PasswordInput NewPassword
    );

    [Authorize]
    [HttpPost("reset/password")]
    [EndpointName("Reset Password")]
    [EndpointDescription("Reset the current user's password.")]
    [MaybeNotFound]
    public async Task<NoContent> ResetPassword(
        [FromBody, Required] ResetPasswordRequest request,
        CancellationToken cancellationToken
    )
    {
        ResetPasswordCommand command = new(request.OldPassword, request.NewPassword, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    public readonly record struct ResetUsernameRequest([property: Required] Username Username);

    [Authorize]
    [HttpPost("reset/username")]
    [EndpointName("Reset Username")]
    [EndpointDescription("Reset the current user's username.")]
    [MaybeNotFound]
    [MaybeConflict]
    public async Task<NoContent> ResetUsername(
        [FromBody, Required] ResetUsernameRequest request,
        CancellationToken cancellationToken
    )
    {
        ResetUsernameCommand command = new(request.Username, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpGet("username/check")]
    [EndpointName("Check Username")]
    [EndpointDescription("Check whether a username is available.")]
    public async Task<Ok<bool>> CheckUsername(
        [FromQuery, Required] [Length(Username.MinLength, Username.MaxLength)] string username,
        CancellationToken cancellationToken
    )
    {
        var query = new UsernameExistenceQuery(Username.Bind(username));
        var result = await mediator.Send(query, cancellationToken);
        return Ok(!result.IsExist);
    }

    #region GitHub OAuth

    const string GitHubScheme = GitHubAuthenticationDefaults.AuthenticationScheme;

    [HttpGet($"oauth/{GitHubScheme}")]
    [EndpointName("GitHub OAuth")]
    [EndpointDescription("Start GitHub OAuth authentication flow.")]
    public async Task<IActionResult> GitHubOAuth()
    {
        var props = new AuthenticationProperties()
        {
            RedirectUri = Url.Action(nameof(GitHubOAuthRedirect)),
        };
        return Challenge(props, GitHubScheme);
    }

    [Authorize(AuthPolicies.OAuth)]
    [HttpGet($"oauth/{GitHubScheme}/redirect")]
    [EndpointName("GitHub OAuth Redirect")]
    [EndpointDescription("Handle GitHub OAuth redirect and issue a JWT token if linked.")]
    public async Task<Results<NoContent, Ok<JwtToken>, UnauthorizedHttpResult>> GitHubOAuthRedirect(
        CancellationToken cancellationToken
    )
    {
        if (User.TryFetchId(out long id) is false)
            return Unauthorized();

        var command = new GitHubLoginCommand(new(id));
        var token = await mediator.Send(command, cancellationToken);

        return token is null ? NoContent() : Ok(token.Value);
    }

    [Authorize]
    [HttpGet($"oauth/{GitHubScheme}/link")]
    [EndpointName("Link GitHub OAuth")]
    [EndpointDescription("Link the authenticated GitHub account to the current user.")]
    public async Task<Results<NoContent, UnauthorizedHttpResult>> GitHubOAuthLink(
        CancellationToken cancellationToken
    )
    {
        if (User.TryFetchId(out long userId) is false)
            return Unauthorized();

        var result = await HttpContext.AuthenticateAsync(GitHubScheme);
        if (
            result is not { Succeeded: true, Principal: { } user }
            || user.TryFetchId(out long githubId) is false
        )
            return Unauthorized();

        var command = new GitHubLinkCommand(new(userId), new(githubId));
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    #endregion
}
