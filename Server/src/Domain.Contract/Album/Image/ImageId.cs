using Domain.ValueObject;

namespace Domain.Album.Image;

[OpenJsonConverter<ImageId>]
public readonly record struct ImageId(long Value) : ITypedId<ImageId, long>
{
    public static ImageId GenerateNew() => new(Snowflake.NewId);
}
