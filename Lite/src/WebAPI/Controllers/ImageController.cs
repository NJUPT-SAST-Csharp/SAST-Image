using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Domain.AlbumAggregate.AlbumEntity;
using Domain.AlbumAggregate.Commands;
using Domain.AlbumAggregate.ImageEntity;
using Domain.Shared;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Query.Images.Queries;
using Storage.Images;
using Storage.Images.Queries;
using WebAPI.Utilities;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Controllers;

[Route("api/v1")]
[ApiController]
public class ImageController(IMediator mediator, IOptions<JsonOptions> jsonOptions)
    : AdvancedController
{
    #region [Command/Post]

    public readonly record struct AddImageRequestMetadata(ImageTitle Title, ImageTags Tags);

    [Authorize]
    [RequestSizeLimit(ImageFile.MaxBytes)]
    [HttpPost("albums/{albumId:long}/add")]
    [EndpointName("Add Image")]
    [EndpointDescription("Add a new image to an album with metadata.")]
    [MaybeNotFound]
    public async Task<Ok<ImageId>> AddImage(
        [FromRoute] AlbumId albumId,
        [FromForm] [Required] [FileValidator(ImageFile.MaxBytes)] IFormFile file,
        [FromForm] [Required] string metadata,
        CancellationToken cancellationToken = default
    )
    {
        var (title, tags) = JsonSerializer.Deserialize<AddImageRequestMetadata>(
            metadata,
            jsonOptions.Value.JsonSerializerOptions
        );

        var image = await file.GetAsync(cancellationToken);
        AddImageCommand command = new(albumId, title, tags, image, User);
        var id = await mediator.Send(command, cancellationToken);

        return Ok(id);
    }

    public readonly record struct UpdateImageRequest(
        ImageTitle? Title = null,
        ImageTags? Tags = null
    );

    [Authorize]
    [HttpPatch("albums/{albumId:long}/images/{imageId:long}")]
    [EndpointName("Update Image Metadata")]
    [EndpointDescription("Update an image's title and tags.")]
    [MaybeNotFound]
    public async Task<NoContent> Update(
        [FromRoute] AlbumId albumId,
        [FromRoute] ImageId imageId,
        [FromBody] UpdateImageRequest request
    )
    {
        UpdateImageCommand command = new(albumId, imageId, request.Title, request.Tags, User);
        await mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPost("albums/{albumId:long}/images/{imageId:long}/remove")]
    [EndpointName("Remove Image")]
    [EndpointDescription("Soft-delete an image from an album.")]
    [MaybeNotFound]
    public async Task<NoContent> Remove(
        [FromRoute] AlbumId albumId,
        [FromRoute] ImageId imageId,
        CancellationToken cancellationToken
    )
    {
        RemoveImageCommand command = new(albumId, imageId, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPost("albums/{albumId:long}/images/{imageId:long}/restore")]
    [EndpointName("Restore Image")]
    [EndpointDescription("Restore a removed image in an album.")]
    [MaybeNotFound]
    public async Task<NoContent> Restore(
        [FromRoute] AlbumId albumId,
        [FromRoute] ImageId imageId,
        CancellationToken cancellationToken
    )
    {
        RestoreImageCommand command = new(albumId, imageId, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPost("albums/{albumId:long}/images/{imageId:long}/like")]
    [EndpointName("Like Image")]
    [EndpointDescription("Like an image in an album.")]
    [MaybeNotFound]
    public async Task<NoContent> Like(
        [FromRoute] AlbumId albumId,
        [FromRoute] ImageId imageId,
        CancellationToken cancellationToken
    )
    {
        LikeImageCommand command = new(albumId, imageId, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("albums/{albumId:long}/images/{imageId:long}/like")]
    [EndpointName("Unlike Image")]
    [EndpointDescription("Remove a like from an image.")]
    [MaybeNotFound]
    public async Task<NoContent> Unlike(
        [FromRoute] AlbumId albumId,
        [FromRoute] ImageId imageId,
        CancellationToken cancellationToken
    )
    {
        UnlikeImageCommand command = new(albumId, imageId, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("albums/{albumId:long}/images/{imageId:long}")]
    [EndpointName("Delete Image")]
    [EndpointDescription("Permanently delete an image from an album.")]
    [MaybeNotFound]
    public async Task<NoContent> Delete(
        [FromRoute] AlbumId albumId,
        [FromRoute] ImageId imageId,
        CancellationToken cancellationToken
    )
    {
        DeleteImageCommand command = new(albumId, imageId, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    #endregion

    #region [Query/Get]


    [HttpGet("images")]
    [ResponseCache(
        Duration = 10,
        Location = ResponseCacheLocation.Any,
        VaryByQueryKeys = ["uploader", "album", "page"]
    )]
    [EndpointName("Get Images")]
    [EndpointDescription("Get images filtered by uploader, album, or page.")]
    public async Task<Ok<ImageDto[]>> GetImages(
        [FromQuery] long? uploader = null,
        [FromQuery] long? album = null,
        [FromQuery] int page = 0,
        CancellationToken cancellationToken = default
    )
    {
        ImagesQuery query = new(uploader, album, page, User);
        var images = await mediator.Send(query, cancellationToken);
        return Ok(images);
    }

    [HttpGet("images/{id:long}")]
    [EndpointName("Get Image File")]
    [EndpointDescription("Get an image file by ID and kind.")]
    public async Task<Results<NotFound, PhysicalFileHttpResult>> GetImageFile(
        [FromRoute] ImageId id,
        [FromQuery] ImageKind kind = ImageKind.Thumbnail,
        CancellationToken cancellationToken = default
    )
    {
        ImageFileQuery query = new(id, kind, User);
        var image = await mediator.Send(query, cancellationToken);
        return image is null ? NotFound() : Image(image.Value);
    }

    [Authorize]
    [HttpGet("albums/{albumId:long}/images/removed")]
    [EndpointName("Get Removed Images")]
    [EndpointDescription("Get removed images for an album.")]
    public async Task<Ok<ImageDto[]>> GetRemovedImages(
        [FromRoute] AlbumId albumId,
        CancellationToken cancellationToken
    )
    {
        RemovedImagesQuery query = new(albumId, User);
        var images = await mediator.Send(query, cancellationToken);
        return Ok(images);
    }

    #endregion
}
