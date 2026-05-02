using System.Diagnostics.CodeAnalysis;
using Domain.Entity;
using Domain.Shared.Converter;

namespace Domain.AlbumAggregate.AlbumEntity;

[OpenJsonConverter<AlbumDescription, string>]
public readonly record struct AlbumDescription(string Value)
    : IValueObject<AlbumDescription, string>,
        IFactoryConstructor<AlbumDescription, string>
{
    public const int MinLength = 3;
    public const int MaxLength = 60;

    public static bool TryCreateNew(
        string value,
        [MaybeNullWhen(false), NotNullWhen(true)] out AlbumDescription newObject
    )
    {
        newObject = default;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        value = value.Trim();

        if (value.Length < MinLength || value.Length > MaxLength)
            return false;

        newObject = new AlbumDescription(value);
        return true;
    }
}
