using Domain.AlbumAggregate.ImageEntity;
using Domain.Shared;
using Mediator;

namespace Storage.Images.Messages;

public sealed record class ImageAddedMessage(DateTime Time, ImageFile File, ImageId ImageId)
    : OutboxMessage(Time),
        IOutboxMessage
{
    public static string Type { get; } = "image_added";

    [Obsolete("ORM only", true)]
    public ImageAddedMessage()
        : this(null!) { }
}

public sealed class ImageAddedMessageHandler(
    IImageFileManager manager,
    ICompressProcessor compressor
) : IOutboxMessageHandler<ImageAddedMessage>
{
    public async ValueTask<Unit> Handle(
        ImageAddedMessage message,
        CancellationToken cancellationToken
    )
    {
        await manager.SaveAsync(message.File, message.ImageId, cancellationToken);
        await compressor.CompressAsync(message.ImageId, "@", cancellationToken);

        return Unit.Value;
    }
}
