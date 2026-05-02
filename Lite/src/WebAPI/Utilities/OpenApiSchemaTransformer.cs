using Domain.Entity;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace WebAPI.Utilities;

public sealed class OpenApiSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(
        OpenApiSchema schema,
        OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken
    )
    {
        if (
            context
                .JsonTypeInfo.Type.GetInterfaces()
                .FirstOrDefault(i =>
                    i.IsGenericType
                    && (
                        i.GetGenericTypeDefinition() == typeof(IValueObject<,>)
                        || i.GetGenericTypeDefinition() == typeof(ITypedId<,>)
                    )
                ) is
            { } type
        )
        {
            var valueType = type.GenericTypeArguments[1];

            var schemaType = valueType switch
            {
                _ when valueType == typeof(string) => JsonSchemaType.String,
                _ when valueType == typeof(int) => JsonSchemaType.Integer,
                _ when valueType == typeof(long) => JsonSchemaType.Integer | JsonSchemaType.String,
                _ when valueType == typeof(decimal) => JsonSchemaType.Integer,
                _ when valueType == typeof(float) => JsonSchemaType.Integer,
                _ when valueType == typeof(double) => JsonSchemaType.Integer,
                _ when valueType == typeof(bool) => JsonSchemaType.Boolean,
                _ when valueType == typeof(DateTime) => JsonSchemaType.String,
                _ when valueType == typeof(Guid) => JsonSchemaType.String,
                _ when valueType.IsEnum => JsonSchemaType.Integer,
                _ when valueType.IsArray => JsonSchemaType.Array,
                _ => JsonSchemaType.Null,
            };

            schema.Type = schemaType;
        }

        return Task.CompletedTask;
    }
}
