using Domain.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Orleans.Concurrency;

namespace Domain.File;

[StatelessWorker]
internal sealed class FileSyncGrain(IDbContextFactory<DomainDbContext> factory)
    : Grain,
        IFileSyncGrain
{
    const int BufferSize = 1024 * 256;

    public async Task<Immutable<byte[]>> GetAsync(
        ImageFileKey key,
        CancellationToken cancellationToken
    )
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);
        await using var connection = (NpgsqlConnection)context.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        NpgsqlLargeObjectManager manager = new(connection);

        var file = await manager.OpenReadAsync(key.Value, cancellationToken); // auto closed when transaction ends

        await using MemoryStream stream = new();
        await file.CopyToAsync(stream, BufferSize, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return stream.ToArray().AsImmutable();
    }

    public async Task<ImageFileKey> UploadAsync(
        Immutable<byte[]> file,
        CancellationToken cancellationToken
    )
    {
        await using var context = await factory.CreateDbContextAsync(cancellationToken);
        await using var connection = (NpgsqlConnection)context.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        NpgsqlLargeObjectManager manager = new(connection);

        var key = await manager.CreateAsync(0, cancellationToken);
        var stream = await manager.OpenReadWriteAsync(key, cancellationToken); // auto closed when transaction ends

        await using MemoryStream ms = new(file.Value);
        await ms.CopyToAsync(stream, BufferSize, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return new(key);
    }
}
