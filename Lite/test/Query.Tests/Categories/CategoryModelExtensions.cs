using System.Runtime.CompilerServices;

namespace Query.Categories;

internal static class CategoryModelExtensions
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    private static extern CategoryModel _Create();

    extension(CategoryModel)
    {
        public static CategoryModel Create(
            long id,
            string name,
            string description
        )
        {
            var category = _Create();

            category.Set(a => a.Id, id);
            category.Set(a => a.Name, name);
            category.Set(a => a.Description, description);

            return category;
        }
    }
}
