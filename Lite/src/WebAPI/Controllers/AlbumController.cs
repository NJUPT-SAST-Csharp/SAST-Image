using System.ComponentModel.DataAnnotations;
using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.Commands;
using Domain.CategoryAggregate.CategoryEntity;
using Domain.Shared;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Query.Albums.Queries;
using Storage.Albums.Queries;
using WebAPI.Utilities;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Controllers;

[Route("api/v1/albums")]
[ApiController]
[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client)]
public sealed class AlbumController(IMediator mediator) : AdvancedController
{
    #region [Command/Post]

    public readonly record struct CreateAlbumRequest(
        [property: Required] AlbumTitle Title,
        [property: Required] AlbumDescription Description,
        [property: Required] CategoryId CategoryId,
        [property: Required] AccessLevel AccessLevel
    );

    [Authorize]
    [HttpPost]
    [EndpointName("Create Album")]
    [EndpointDescription("Create a new album for the current user.")]
    public async Task<Ok<AlbumId>> Create(
        [FromBody, Required] CreateAlbumRequest request,
        CancellationToken cancellationToken
    )
    {
        CreateAlbumCommand command = new(
            request.Title,
            request.Description,
            request.AccessLevel,
            request.CategoryId,
            User
        );
        var id = await mediator.Send(command, cancellationToken);
        return Ok(id);
    }

    [Authorize]
    [HttpPost("{id:long}/remove")]
    [EndpointName("Remove Album")]
    [EndpointDescription("Soft-delete an album by ID.")]
    [MaybeNotFound]
    public async Task<NoContent> Remove([FromRoute] AlbumId id, CancellationToken cancellationToken)
    {
        RemoveAlbumCommand command = new(id, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPost("{id:long}/restore")]
    [EndpointName("Restore Album")]
    [EndpointDescription("Restore a previously removed album by ID.")]
    [MaybeNotFound]
    public async Task<NoContent> Restore(
        [FromRoute] AlbumId id,
        CancellationToken cancellationToken
    )
    {
        RestoreAlbumCommand command = new(id, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    public readonly record struct UpdateAccessLevelRequest(
        [property: Required] AccessLevel AccessLevel
    );

    [Authorize]
    [HttpPost("{id:long}/accessLevel")]
    [EndpointName("Update Album Access Level")]
    [EndpointDescription("Update an album's access level.")]
    [MaybeNotFound]
    public async Task<NoContent> UpdateAccessLevel(
        [FromRoute] AlbumId id,
        [FromBody, Required] UpdateAccessLevelRequest request,
        CancellationToken cancellationToken
    )
    {
        UpdateAccessLevelCommand command = new(id, request.AccessLevel, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    public readonly record struct UpdateInfoRequest(
        AlbumTitle? Title = null,
        AlbumDescription? Description = null,
        AlbumTags? Tags = null
    );

    [Authorize]
    [HttpPatch("{id:long}/info")]
    [EndpointName("Update Album Info")]
    [EndpointDescription("Update an album's title, description, or tags.")]
    [MaybeNotFound]
    public async Task<NoContent> UpdateInfo(
        [FromRoute] AlbumId id,
        [FromBody, Required] UpdateInfoRequest request,
        CancellationToken cancellationToken
    )
    {
        UpdateAlbumInfoCommand command = new(
            id,
            request.Title,
            request.Description,
            request.Tags,
            User
        );
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:long}/cover")]
    [RequestFormLimits(MultipartBodyLengthLimit = ImageFile.MaxBytes)]
    [EndpointName("Update Album Cover")]
    [EndpointDescription("Update or clear an album's cover image.")]
    [MaybeNotFound]
    public async Task<NoContent> UpdateCover(
        [FromRoute] AlbumId id,
        [FromForm] [FileValidator(ImageFile.MaxBytes)] IFormFile? file = null,
        CancellationToken cancellationToken = default
    )
    {
        ImageFile? cover = null;
        if (file is not null)
        {
            cover = await file.GetAsync(cancellationToken);
        }

        UpdateCoverCommand command = new(id, cover, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPost("{id:long}/subscribe")]
    [EndpointName("Subscribe Album")]
    [EndpointDescription("Subscribe the current user to an album.")]
    [MaybeNotFound]
    public async Task<NoContent> Subscribe(
        [FromRoute] AlbumId id,
        CancellationToken cancellationToken
    )
    {
        SubscribeCommand command = new(id, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:long}/subscribe")]
    [EndpointName("Unsubscribe Album")]
    [EndpointDescription("Unsubscribe the current user from an album.")]
    [MaybeNotFound]
    public async Task<NoContent> Unsubscribe(
        [FromRoute] AlbumId id,
        CancellationToken cancellationToken
    )
    {
        UnsubscribeCommand command = new(id, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    #endregion

    #region [Query/Get]

    [HttpGet]
    [ResponseCache(
        Duration = 10,
        Location = ResponseCacheLocation.Any,
        VaryByQueryKeys = ["category", "author", "title"]
    )]
    [EndpointName("Get Albums")]
    [EndpointDescription("Get albums filtered by category, author, or title.")]
    public async Task<Ok<AlbumDto[]>> GetAlbums(
        [FromQuery] long? category = null,
        [FromQuery] long? author = null,
        [FromQuery] [MaxLength(AlbumTitle.MaxLength)] string? title = null,
        [FromQuery] long? cursor = null,
        CancellationToken cancellationToken = default
    )
    {
        var result = await mediator.Send(
            new AlbumsQuery(category, author, title, cursor, User),
            cancellationToken
        );
        return Ok(result);
    }

    [HttpGet("removed")]
    [ResponseCache(NoStore = true)]
    [EndpointName("Get Removed Albums")]
    [EndpointDescription("Get albums removed by the current user.")]
    public async Task<Ok<RemovedAlbumDto[]>> GetRemovedAlbums()
    {
        var result = await mediator.Send(new RemovedAlbumsQuery(User));
        return Ok(result);
    }

    [HttpGet("{id:long}/cover")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    [EndpointName("Get Album Cover")]
    [EndpointDescription("Get an album's cover image by ID.")]
    public async Task<Results<NotFound, PhysicalFileHttpResult>> GetCover(
        [FromRoute] AlbumId id,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(new AlbumCoverQuery(id, User), cancellationToken);
        return result is null ? NotFound() : Image(result.Value);
    }

    #endregion
}
