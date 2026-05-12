using Domain.ValueObject;

namespace Domain.User;

[OpenJsonConverter<UserId>]
public readonly record struct UserId(long Value) : ITypedId<UserId>
{
    public static UserId GenerateNew() => new(Snowflake.NewId);

    public static implicit operator UserId(long value) => new(value);
}
