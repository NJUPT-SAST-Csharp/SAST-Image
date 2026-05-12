namespace Domain.Event;

[Alias("domain_event")]
[Immutable]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed class DomainEventUnit
{
    public Guid EventId { get; init; } = Guid.CreateVersion7();
    public required long GrainId { get; init; }
    public required int ETag { get; init; }
    public required DomainEventBase Value { get; init; }
    public required DateTime Timestamp { get; init; }
    public string Type => Value.GetType().Name;
}
