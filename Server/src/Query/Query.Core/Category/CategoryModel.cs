namespace Query.Category;

public sealed class CategoryModel
{
    public required long Id { get; init; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
