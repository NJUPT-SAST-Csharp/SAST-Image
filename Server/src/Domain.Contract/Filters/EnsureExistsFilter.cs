using System.Reflection;

namespace Domain.Filters;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class AllowRecordNotExistsAttribute : Attribute;

public sealed class EnsureExistsFilter : IOutgoingGrainCallFilter
{
    public Task Invoke(IOutgoingGrainCallContext context)
    {
        if (
            context.InterfaceMethod.GetCustomAttribute<AllowRecordNotExistsAttribute>() is not null
            || context.Grain is not IDomainGrainStateRecordExistenceIndicator grain
            || context.TargetId.TryGetIntegerKey(out var id, out _) is false
        )
            return context.Invoke();

        if (grain.RecordExists is false)
            throw new NotFoundException(id);

        return context.Invoke();
    }
}

[Alias("404_exception")]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed class NotFoundException(long id)
    : Exception($"The record with id {id} does not exist.")
{
    public long Id { get; } = id;
}

public interface IDomainGrainStateRecordExistenceIndicator
{
    public bool RecordExists { get; }
}
