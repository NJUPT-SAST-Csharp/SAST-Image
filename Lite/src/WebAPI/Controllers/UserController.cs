using System.ComponentModel.DataAnnotations;
using Domain.Shared;
using Domain.UserAggregate.Commands.Profile;
using Domain.UserAggregate.UserEntity;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Query.Users.Queries;
using Storage.Users.Queries;
using WebAPI.Utilities;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Controllers;

[Route("api/v1/users")]
[ApiController]
public sealed class UserController(IMediator mediator) : AdvancedController
{
    #region [Command/Post]


    public readonly record struct UpdateProfileRequest(
        Nickname? Nickname = null,
        Biography? Biography = null
    );

    [Authorize]
    [HttpPatch("profile")]
    [EndpointName("Update Profile")]
    [EndpointDescription("Update the current user's profile information.")]
    [MaybeNotFound]
    public async Task<NoContent> UpdateProfile(
        [FromBody, Required] UpdateProfileRequest request,
        CancellationToken cancellationToken
    )
    {
        UpdateProfileCommand command = new(request.Nickname, request.Biography, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPut("avatar")]
    [RequestSizeLimit(ImageFile.MaxBytes)]
    [EndpointName("Update Avatar")]
    [EndpointDescription("Update the current user's avatar image.")]
    [MaybeNotFound]
    public async Task<NoContent> UpdateAvatar(
        [FromForm, Required] [FileValidator(ImageFile.MaxBytes)] IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var avatar = await file.GetAsync(cancellationToken);
        UpdateAvatarCommand command = new(avatar, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPut("header")]
    [RequestSizeLimit(ImageFile.MaxBytes)]
    [EndpointName("Update Header")]
    [EndpointDescription("Update the current user's header image.")]
    [MaybeNotFound]
    public async Task<NoContent> UpdateHeader(
        [FromForm, Required] [FileValidator(ImageFile.MaxBytes)] IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var header = await file.GetAsync(cancellationToken);
        UpdateHeaderCommand command = new(header, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    #endregion

    #region [Query/Get]

    [HttpGet("{id:long}/avatar")]
    [EndpointName("Get User Avatar")]
    [EndpointDescription("Get a user's avatar image by user ID.")]
    public async Task<Results<NotFound, PhysicalFileHttpResult>> GetAvatar(
        [FromRoute] UserId id,
        CancellationToken cancellationToken
    )
    {
        var query = new UserAvatarQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return result is null ? NotFound() : Image(result.Value);
    }

    [HttpGet("{id:long}/header")]
    [EndpointName("Get User Header")]
    [EndpointDescription("Get a user's header image by user ID.")]
    public async Task<Results<NotFound, PhysicalFileHttpResult>> GetHeader(
        [FromRoute] UserId id,
        CancellationToken cancellationToken
    )
    {
        var query = new UserHeaderQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return result is null ? NotFound() : Image(result.Value);
    }

    [HttpGet("{id:long}/profile")]
    [EndpointName("Get User Profile")]
    [EndpointDescription("Get a user's public profile information by user ID.")]
    public async Task<Results<NotFound, Ok<UserProfileDto>>> GetProfileInfo(
        [FromRoute] UserId id,
        CancellationToken cancellationToken
    )
    {
        var query = new UserProfileQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    #endregion
}
