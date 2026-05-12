using System.Diagnostics.CodeAnalysis;
using Domain.ValueObject;

namespace Domain.File;

[OpenJsonConverter<ImageFileKey, uint>, Immutable]
public readonly record struct ImageFileKey(uint Value) : IValueObject<ImageFileKey, uint>
{
    public static bool TryCreateNew(uint input, [NotNullWhen(true)] out ImageFileKey newObject)
    {
        newObject = new(input);
        return true;
    }
}
