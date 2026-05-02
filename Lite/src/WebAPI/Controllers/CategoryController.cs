using System.ComponentModel.DataAnnotations;
using Domain.CategoryAggregate.CategoryEntity;
using Domain.CategoryAggregate.Commands;
using Infrastructure;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Query.Categories.Queries;
using WebAPI.Utilities;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Controllers;

[Route("api/v1/categories")]
[ApiController]
[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client)]
public sealed class CategoryController(IMediator mediator) : AdvancedController
{
    #region [Command/Post]

    public readonly record struct CreateCategoryRequest(
        [property: Required] CategoryName Name,
        [property: Required] CategoryDescription Description
    );

    [HttpPost]
    [Authorize(AuthPolicies.Admin)]
    [EndpointName("Create Category")]
    [EndpointDescription("Create a new category.")]
    [MaybeConflict]
    public async Task<Ok<CategoryId>> Create(
        [FromBody, Required] CreateCategoryRequest request,
        CancellationToken cancellationToken
    )
    {
        CreateCategoryCommand command = new(request.Name, request.Description, User);
        var id = await mediator.Send(command, cancellationToken);
        return Ok(id);
    }

    public readonly record struct UpdateCategoryRequest(
        CategoryName? Name = null,
        CategoryDescription? Description = null
    );

    [HttpPatch("{id:long}")]
    [Authorize(AuthPolicies.Admin)]
    [EndpointName("Update Category")]
    [EndpointDescription("Update an existing category.")]
    [MaybeConflict]
    public async Task<NoContent> Update(
        [FromRoute] CategoryId id,
        [FromBody, Required] UpdateCategoryRequest request,
        CancellationToken cancellationToken
    )
    {
        UpdateCategoryCommand command = new(id, request.Name, request.Description, User);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    //[HttpPost("{id:long}/remove")]
    //public async Task<IActionResult> Remove(
    //    [FromRoute] long id,
    //    CancellationToken cancellationToken
    //)
    //{
    //    var command = new RemoveCategoryCommand(new(id), User);
    //    await mediator.Send(command, cancellationToken);
    //    return NoContent();
    //}

    #endregion

    #region [Query/Get]

    [HttpGet]
    [EndpointName("Get Categories")]
    [EndpointDescription("Get all categories.")]
    public async Task<Ok<CategoryDto[]>> Get(CancellationToken cancellationToken)
    {
        CategoriesQuery query = new();
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    //[HttpGet("{id:long}")]
    //public async Task<IActionResult> Get([FromRoute] long id, CancellationToken cancellationToken)
    //{
    //    var query = new GetCategoryQuery(new(id));
    //    var result = await mediator.Send(query, cancellationToken);
    //    return Ok(result);
    //}

    #endregion
}
