using System.Text.Json.Serialization;
using Domain.Album;
using Domain.Category;
using Domain.Event;
using Domain.User;

namespace Domain;

public sealed class DomainStateUnit
{
    public required long Id { get; set; }
    public required int ETag { get; set; }
    public required DomainStateBase Value { get; init; }
}

[JsonPolymorphic(
    TypeDiscriminatorPropertyName = "$type",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
)]
[DerivedState<CategoryState>]
[DerivedState<AlbumState>]
[DerivedState<UserState>]
public abstract class DomainStateBase
{
    public bool RecordExists { get; set; } = false;
    public abstract void Apply(DomainEventBase e);
}

internal sealed class DerivedStateAttribute<TState> : DerivedAttribute<TState, DomainStateBase>
    where TState : DomainStateBase;
