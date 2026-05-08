using Domain.ValueObject;

namespace Domain.Album;

[OpenJsonConverter<AlbumId>]
public readonly record struct AlbumId(long Value) : ITypedId<AlbumId, long>
{
    public static AlbumId GenerateNew() => new(Snowflake.NewId);

    public static implicit operator AlbumId(long value) => new(value);
}
