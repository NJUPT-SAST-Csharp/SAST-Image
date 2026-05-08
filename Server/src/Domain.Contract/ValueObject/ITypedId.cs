namespace Domain.ValueObject;

public interface ITypedId<TId> : ITypedId<TId, long>
    where TId : ITypedId<TId, long> { }

public interface ITypedId<TId, TValue> : IEquatable<TId>
    where TValue : IEquatable<TValue>
    where TId : ITypedId<TId, TValue>
{
    public TValue Value { get; init; }

    public static abstract TId GenerateNew();
}
