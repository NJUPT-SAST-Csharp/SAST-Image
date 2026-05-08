using System.Reflection;

namespace Domain.Filters;

/// <summary>
/// Indicates that a method is associated with a unique identifier for discovery or tracking purposes.
/// </summary>
/// <remarks>
/// Apply this attribute to methods that require a unique identifier without duplication, such as for creating new stuff.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
internal sealed class EnsureUniqueIdAttribute : Attribute;

public sealed class EnsureUniqueIdFilter(IIdUniquenessChecker checker) : IOutgoingGrainCallFilter
{
    public async Task Invoke(IOutgoingGrainCallContext context)
    {
        if (context.InterfaceMethod.GetCustomAttribute<EnsureUniqueIdAttribute>() is null)
        {
            await context.Invoke();
            return;
        }
        if (context.TargetId.TryGetIntegerKey(out var id, out _) is false)
            throw new InvalidOperationException(
                "UniqueIdAttribute can only be applied to grains with integer keys."
            );

        var cancellationToken = context.Request.GetCancellationToken();
        if (await checker.ExistsAsync(id, cancellationToken))
            throw new UniqueIdException(id);

        await context.Invoke();
        return;
    }
}

[Alias("409_exception")]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed class UniqueIdException(long id)
    : Exception($"An entity with the unique identifier {id} already exists.")
{
    public long Id { get; } = id;
}

public interface IIdUniquenessChecker
{
    ValueTask<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);
}
