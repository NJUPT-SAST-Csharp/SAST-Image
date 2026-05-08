namespace Domain.Album;

[Alias("exception_category_not_found")]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
internal sealed class CategoryNotFoundException(CategoryId id)
    : Exception($"Category with id {id} not found.")
{
    public CategoryId Id { get; init; } = id;
}
