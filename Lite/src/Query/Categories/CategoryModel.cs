using Domain.CategoryAggregate.Events;

namespace Query.Categories;

public sealed class CategoryModel
{
    [Obsolete("For ORM", true)]
    private CategoryModel() { }

    internal CategoryModel(CategoryCreatedEvent e)
    {
        Id = e.Id.Value;
        Name = e.Name.Value;
        Description = e.Description.Value;
    }

    public long Id { get; init; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
