using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Utilities.Attributes;

public sealed class MaybeConflictAttribute()
    : ProducesResponseTypeAttribute<ProblemDetails>(
        StatusCodes.Status409Conflict,
        "application/problem+json"
    );

public sealed class MaybeNotFoundAttribute()
    : ProducesResponseTypeAttribute<ProblemDetails>(
        StatusCodes.Status404NotFound,
        "application/problem+json"
    );
