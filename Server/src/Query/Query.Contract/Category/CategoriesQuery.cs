using Mediator;

namespace Query.Category;

[Alias(nameof(CategoryDto))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public record class CategoryDto(long Id, string Name, string Description);

[Alias(nameof(CategoriesQuery))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed record class CategoriesQuery(long? Id = null) : IQuery<CategoryDto[]>;
