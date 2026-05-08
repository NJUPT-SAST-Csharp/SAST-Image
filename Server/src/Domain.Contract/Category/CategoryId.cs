using Domain.ValueObject;

namespace Domain.Category;

[OpenJsonConverter<CategoryId>]
public readonly record struct CategoryId(long Value) : ITypedId<CategoryId, long>
{
    public static CategoryId GenerateNew() => new(Snowflake.NewId);

    public static implicit operator CategoryId(long Value) => new(Value);
}
