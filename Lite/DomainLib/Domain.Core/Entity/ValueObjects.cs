using System.Collections;

namespace Domain.Entity;

public interface IValueObjectsBase { }

public abstract class ValueObjects<TObject, TValue>
    : IValueObject<TObject, TValue[]>,
        IValueObjectsBase,
        IEnumerable<TValue>
    where TObject : ValueObjects<TObject, TValue>
{
    private TValue[] _value = [];
    public TValue[] Value
    {
        get => _value;
        init => _value = value;
    }
    public int Count => Value.Length;
    public bool IsEmpty => Value.Length == 0;
    public TValue this[int index] => Value[index];

    /// <summary>
    /// Determines whether the current object is equal to another object of the same type based on the equality of their
    /// value collections.
    /// </summary>
    /// <remarks>
    /// Equality is determined by comparing the sets of values in both objects, regardless of order or duplicates. The
    /// comparison uses the default equality comparer for the value type.
    /// </remarks>
    /// <param name="other">The object to compare with the current object. Can be null.</param>
    /// <returns>true if the value collections of both objects contain the same elements; otherwise, false.</returns>
    public virtual bool Equals(TObject? other)
    {
        if (ReferenceEquals(this, other))
            return true;
        if (other is null)
            return false;
        if (Count == 0 && other.Count == 0)
            return true;

        var set = new HashSet<TValue>(Value, EqualityComparer<TValue>.Default);
        return set.SetEquals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is TObject valueObjects && Equals(valueObjects);
    }

    public static bool operator ==(
        ValueObjects<TObject, TValue> left,
        ValueObjects<TObject, TValue> right
    )
    {
        return left.Equals(right);
    }

    public static bool operator !=(
        ValueObjects<TObject, TValue> left,
        ValueObjects<TObject, TValue> right
    )
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (int i = 0; i < Value.Length; i++)
            yield return Value[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class ValueObjectsExtensions
{
    extension<TObject>(TObject)
        where TObject : IValueObjectsBase, new()
    {
        public static TObject Empty => new();
    }
}
