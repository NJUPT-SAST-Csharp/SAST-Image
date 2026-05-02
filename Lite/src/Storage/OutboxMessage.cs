using Mediator;

namespace Storage;

public abstract record class OutboxMessage(DateTime Time) : IRequest
{
    public virtual Guid Id { get; } = Guid.CreateVersion7();
    public MessageStatus Status { get; set; } = MessageStatus.Staging;
    public int RetryCount { get; set; } = 0;
    public DateTime? RetryAt { get; set; } = null;

    public enum MessageStatus : byte
    {
        Unknown = 0,
        Staging,
        Pending,
        Completed,
        Failed,
    }
}

internal interface IOutboxMessage
{
    public static abstract string Type { get; }
}

internal interface IOutboxMessageHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : OutboxMessage { }
