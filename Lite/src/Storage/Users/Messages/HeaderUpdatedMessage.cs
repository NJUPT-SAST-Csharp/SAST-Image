using Domain.Shared;
using Domain.UserAggregate.UserEntity;
using Mediator;

namespace Storage.Users.Messages;

public sealed record class HeaderUpdatedMessage(DateTime Time, ImageFile Image, UserId UserId)
    : OutboxMessage(Time),
        IOutboxMessage
{
    public static string Type { get; } = "header_updated";

    [Obsolete("ORM only", true)]
    public HeaderUpdatedMessage()
        : this(null!) { }
}

public sealed class HeaderUpdatedMessageHandler(
    IImageFileManager manager,
    ICompressProcessor compressor
) : IOutboxMessageHandler<HeaderUpdatedMessage>
{
    public async ValueTask<Unit> Handle(
        HeaderUpdatedMessage message,
        CancellationToken cancellationToken
    )
    {
        const string extension = "header";

        await manager.SaveAsync(message.Image, message.UserId, extension, cancellationToken);
        await compressor.CompressAsync(message.UserId, extension, cancellationToken);

        return Unit.Value;
    }
}
