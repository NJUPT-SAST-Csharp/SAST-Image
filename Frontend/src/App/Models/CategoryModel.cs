using Domain.Api;

namespace App.Models;

public sealed class CategoryModel
{
    public long Id { get; init; }
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";

    public static implicit operator CategoryModel(CategoryResponse r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        Description = r.Description,
    };
}
