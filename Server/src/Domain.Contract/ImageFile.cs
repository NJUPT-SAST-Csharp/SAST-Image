using System.Diagnostics.CodeAnalysis;
using Domain.ValueObject;

namespace Domain;

/// <summary>
/// Represents a value object that encapsulates the path to an existing image file.
/// </summary>
/// <remarks>
/// An ImageFile instance guarantees that the underlying file path refers to an existing file at the time of creation.
/// Use the TryCreateNew method to safely construct an ImageFile from a string path. This type is immutable and can be
/// used to enforce file existence constraints in domain models.
/// </remarks>
public readonly record struct ImageFile(string Value) : IValueObject<ImageFile, string>
{
    public const int MaxBytes = 1024 * 1024 * 50; // 50 MB

    public bool TryGetValue([MaybeNullWhen(false)] out string value)
    {
        if (File.Exists(Value))
        {
            value = Value;
            return true;
        }
        value = default!;
        return false;
    }

    public static bool TryCreateNew(string input, [NotNullWhen(true)] out ImageFile newObject)
    {
        newObject = default;

        if (File.Exists(input) is false)
        {
            return false;
        }

        newObject = new ImageFile() { Value = input };
        return true;
    }
}
