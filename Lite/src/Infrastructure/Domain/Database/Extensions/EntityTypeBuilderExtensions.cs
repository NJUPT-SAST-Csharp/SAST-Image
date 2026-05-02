using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Database.Extensions;

internal static class EntityTypeBuilderExtensions
{
    extension(EntityTypeBuilder builder)
    {
        public PropertyBuilder<TProperty> Field<TProperty>(string name)
        {
            return builder.Property<TProperty>(name);
        }

        public PropertyBuilder<TProperty> Field<TProperty>(string name, string columnName)
        {
            return builder.Field<TProperty>(name).HasColumnName(columnName);
        }
    }
}

internal static class OwnedNavigationBuilderExtensions
{
    extension(OwnedNavigationBuilder builder)
    {
        public PropertyBuilder<TProperty> Field<TProperty>(string name)
        {
            return builder.Property<TProperty>(name);
        }

        public PropertyBuilder<TProperty> Field<TProperty>(string name, string columnName)
        {
            return builder.Field<TProperty>(name).HasColumnName(columnName);
        }
    }
}
