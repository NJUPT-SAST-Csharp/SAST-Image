using Domain.Filters;

namespace Domain.Category;

[Alias("category_grain")]
public interface ICategoryGrain : IGrainWithIntegerKey
{
    [AccessControl(Role.Admin)]
    [EnsureUniqueId]
    [AllowRecordNotExists]
    [Alias("category_create")]
    ValueTask<CategoryId> Create(CategoryName name, CategoryDescription description);

    [AccessControl(Role.Admin)]
    [Alias("category_update")]
    ValueTask Update(CategoryName? name, CategoryDescription? description);

    [AccessControl(Role.Admin)]
    [Alias("category_delete")]
    ValueTask Delete();

    [Alias("category_check")]
    ValueTask<bool> Exists();
}
