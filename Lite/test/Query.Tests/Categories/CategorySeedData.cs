namespace Query.Categories;

internal sealed class CategorySeedData
{
    public static IEnumerable<CategoryModel> Seed =>
        [
            CategoryModel.Create(
                id: IDs.Categories.General,
                name: "Category 1",
                description: "Description 1"
            ),
            CategoryModel.Create(
                id: IDs.Categories.Nature,
                name: "Nature",
                description: "Landscapes, wildlife, and nature scenes."
            ),
            CategoryModel.Create(
                id: IDs.Categories.Urban,
                name: "Urban",
                description: "City life, architecture, and street photography."
            ),
            CategoryModel.Create(
                id: IDs.Categories.Portrait,
                name: "Portrait",
                description: "People and portrait photography."
            ),
        ];
}
