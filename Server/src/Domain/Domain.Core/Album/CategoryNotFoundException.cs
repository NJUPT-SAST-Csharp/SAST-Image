using Domain.Category;

namespace Domain.Album;

[Alias("category_not_found_exception")]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
internal sealed class CategoryNotFoundException(CategoryId id)
    : Exception($"Category with id {id} not found.")
{
    public CategoryId Id { get; init; } = id;
}
