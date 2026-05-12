namespace Shared.Core;

public sealed record class Checkpoint
{
    public static readonly Guid CursorId = Guid.Empty;

    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTime Timestamp { get; set; }
    public long? GrainId { get; init; } = null;
    public CheckpointStatus Status { get; init; } = CheckpointStatus.Success;

    // Optimistic concurrency control, used to detect if the checkpoint has been updated by another query service instance
    //public uint Version { get; init; }
}

public enum CheckpointStatus : byte
{
    Failed = 0,
    Success = 1,
}
