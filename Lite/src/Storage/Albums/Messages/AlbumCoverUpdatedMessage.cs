using Domain.AlbumAggregate.AlbumEntity;
using Domain.Shared;
using Mediator;

namespace Storage.Albums.Messages;

public sealed record class AlbumCoverUpdatedMessage(DateTime Time, AlbumId Album, ImageFile? File)
    : OutboxMessage(Time),
        IOutboxMessage
{
    public static string Type => "cover_updated";
}

public sealed class AlbumCoverUpdatedMessageHandler(
    IImageFileManager manager,
    ICompressProcessor compressor
) : IOutboxMessageHandler<AlbumCoverUpdatedMessage>
{
    public async ValueTask<Unit> Handle(
        AlbumCoverUpdatedMessage message,
        CancellationToken cancellationToken
    )
    {
        var (_, album, file) = message;

        if (file is null)
        {
            await manager.DeleteAsync(message.Album, cancellationToken);
            return Unit.Value;
        }

        await manager.SaveAsync(file.Value, album, cancellationToken);
        await compressor.CompressAsync(album, cancellationToken);

        return Unit.Value;
    }
}
