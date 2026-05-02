using Domain.Shared;
using Domain.UserAggregate.UserEntity;
using Mediator;

namespace Storage.Users.Messages;

public sealed record class AvatarUpdatedMessage(DateTime Time, ImageFile Image, UserId UserId)
    : OutboxMessage(Time),
        IOutboxMessage
{
    public static string Type { get; } = "avatar_updated";

    [Obsolete("ORM only", true)]
    public AvatarUpdatedMessage()
        : this(null!) { }
}

public sealed class AvatarUpdatedMessageHandler(
    ICompressProcessor compressor,
    IImageFileManager manager
) : IOutboxMessageHandler<AvatarUpdatedMessage>
{
    public async ValueTask<Unit> Handle(
        AvatarUpdatedMessage message,
        CancellationToken cancellationToken
    )
    {
        const string extension = "avatar";

        await manager.SaveAsync(message.Image, message.UserId, extension, cancellationToken);
        await compressor.CompressAsync(message.UserId, extension, cancellationToken);

        return Unit.Value;
    }
}
