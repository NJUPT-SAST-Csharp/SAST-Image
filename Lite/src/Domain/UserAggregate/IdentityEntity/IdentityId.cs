using Domain.Entity;

namespace Domain.UserAggregate.IdentityEntity;

/// <summary>
/// Represents a strongly-typed external (OAuth) identifier value object.
/// </summary>
/// <remarks>
/// Use this type to encapsulate external identifiers and provide type safety when working with IDs from external systems.
/// <br/>
/// This struct is immutable and supports value-based equality.
/// <br/>
/// Currently, this struct is defined with a <seealso cref="long"/> value for <strong>GitHub</strong>.
/// </remarks>
public readonly record struct IdentityId(long Value)
    : ITypedId<IdentityId, long, long>,
        IValueObject<IdentityId, long>
{
    public static IdentityId GenerateNew(long value)
    {
        return new IdentityId(value);
    }
}
