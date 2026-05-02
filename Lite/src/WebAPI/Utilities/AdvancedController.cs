using Domain.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Utilities;

[ProducesErrorResponseType(typeof(ProblemDetails))]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
public abstract class AdvancedController : ControllerBase
{
    public Ok<TResult> Ok<TResult>(TResult result) => TypedResults.Ok(result);

    public new NoContent NoContent() => TypedResults.NoContent();

    public new NotFound NotFound() => TypedResults.NotFound();

    public PhysicalFileHttpResult Image(ImageFile file) =>
        TypedResults.PhysicalFile(
            file.Value,
            contentType: "image/*",
            fileDownloadName: file.DownloadName
        );

    public new UnauthorizedHttpResult Unauthorized() => TypedResults.Unauthorized();
}
