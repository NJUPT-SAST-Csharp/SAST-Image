using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Database.Extensions;

internal static class ModelConfigurationBuilderExtensions
{
    extension(ModelConfigurationBuilder builder)
    {
        public PropertiesConfigurationBuilder<TObject> MapValueObject<TObject, TValue>()
            where TObject : IValueObject<TObject, TValue>, new() =>
            builder.Properties<TObject>().HaveConversion<ValueObjectConverter<TObject, TValue>>();

        public PropertiesConfigurationBuilder<TObject> MapValueObjects<TObject, TValue>()
            where TObject : ValueObjects<TObject, TValue>, new() =>
            builder
                .Properties<TObject>()
                .HaveConversion<
                    ValueObjectsConverter<TObject, TValue>,
                    ValueObjectsComparer<TObject, TValue>
                >();

        public PropertiesConfigurationBuilder<TObject> MapValueObjects<TObject, TElement, TValue>()
            where TObject : ValueObjects<TObject, TElement>, new()
            where TElement : ITypedId<TElement, TValue>, new()
            where TValue : IEquatable<TValue> =>
            builder
                .Properties<TObject>()
                .HaveConversion<
                    ValueObjectsConverter<TObject, TElement, TValue>,
                    ValueObjectsComparer<TObject, TElement, TValue>
                >();

        public PropertiesConfigurationBuilder<TId> MapTypedId<TId, TValue>()
            where TId : ITypedId<TId, TValue>, new()
            where TValue : IEquatable<TValue> =>
            builder.Properties<TId>().HaveConversion<TypedIdConverter<TId, TValue>>();

        public PropertiesConfigurationBuilder<TId> MapTypedId<TId>()
            where TId : ITypedId<TId, long>, new() => builder.MapTypedId<TId, long>();
    }
}

file sealed class ValueObjectConverter<TObject, TValue>()
    : ValueConverter<TObject, TValue>(id => id.Value, value => new() { Value = value })
    where TObject : IValueObject<TObject, TValue>, new() { }

file sealed class ValueObjectsConverter<TObject, TValue>()
    : ValueConverter<TObject, TValue[]>(obj => obj.Value, value => new() { Value = value })
    where TObject : ValueObjects<TObject, TValue>, new() { }

file sealed class ValueObjectsConverter<TObject, TElement, TValue>()
    : ValueConverter<TObject, TValue[]>(
        objs => Array.ConvertAll(objs.Value, elem => elem.Value),
        values =>
            new() { Value = Array.ConvertAll(values, value => new TElement() { Value = value }) }
    )
    where TObject : ValueObjects<TObject, TElement>, new()
    where TElement : ITypedId<TElement, TValue>, new()
    where TValue : IEquatable<TValue> { }

file sealed class ValueObjectsComparer<TObject, TValue>()
    : ValueComparer<TObject>(
        (c1, c2) => EqualityComparer<TObject>.Default.Equals(c1, c2),
        c => c.GetHashCode()
    )
    where TObject : ValueObjects<TObject, TValue>, new() { }

file sealed class ValueObjectsComparer<TObject, TElement, TValue>()
    : ValueComparer<TObject>(
        (c1, c2) => EqualityComparer<TObject>.Default.Equals(c1, c2),
        c => c.GetHashCode()
    )
    where TObject : ValueObjects<TObject, TElement>, new()
    where TElement : ITypedId<TElement, TValue>, new()
    where TValue : IEquatable<TValue> { }

file sealed class TypedIdConverter<TId, TValue>()
    : ValueConverter<TId, TValue>(id => id.Value, value => new() { Value = value })
    where TId : ITypedId<TId, TValue>, new()
    where TValue : IEquatable<TValue> { }
