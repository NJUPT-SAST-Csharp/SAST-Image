using System.Diagnostics.CodeAnalysis;

namespace Domain.ValueObject;

public interface IValueObject<TObject, TValue>
    where TObject : IValueObject<TObject, TValue>
{
    public TValue Value { get; init; }

    public static abstract bool TryCreateNew(
        TValue input,
        [NotNullWhen(true)] out TObject? newObject
    );
}
