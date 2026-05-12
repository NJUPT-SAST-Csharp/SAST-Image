using System.Data;
using System.Text;
using Npgsql;

namespace Domain.File;

/// <summary>
/// An interface to remotely control the seekable stream for an opened large object on a PostgreSQL server.
/// Note that the OpenRead/OpenReadWrite method as well as all operations performed on this stream must be wrapped inside a database transaction.
/// </summary>
public sealed class NpgsqlLargeObjectStream : Stream
{
    readonly NpgsqlLargeObjectManager _manager = null!;
    readonly int _fd;
    long _pos;
    readonly bool _writeable;
    bool _disposed;

    internal NpgsqlLargeObjectStream(NpgsqlLargeObjectManager manager, int fd, bool writeable)
    {
        _manager = manager;
        _fd = fd;
        _pos = 0;
        _writeable = writeable;
    }

    void CheckDisposed()
    {
        if (_disposed)
            throw new InvalidOperationException("Object disposed");
    }

    /// <summary>
    /// Since PostgreSQL 9.3, large objects larger than 2GB can be handled, up to 4TB.
    /// This property returns true whether the PostgreSQL version is >= 9.3.
    /// </summary>
    public bool Has64BitSupport => true;

    /// <summary>
    /// Reads <i>count</i> bytes from the large object. The only case when fewer bytes are read is when end of stream is reached.
    /// </summary>
    /// <param name="buffer">The buffer where read data should be stored.</param>
    /// <param name="offset">The offset in the buffer where the first byte should be read.</param>
    /// <param name="count">The maximum number of bytes that should be read.</param>
    /// <returns>How many bytes actually read, or 0 if end of file was already reached.</returns>
    public override int Read(byte[] buffer, int offset, int count) =>
        Read(async: false, buffer, offset, count).GetAwaiter().GetResult();

    /// <summary>
    /// Reads <i>count</i> bytes from the large object. The only case when fewer bytes are read is when end of stream is reached.
    /// </summary>
    /// <param name="buffer">The buffer where read data should be stored.</param>
    /// <param name="offset">The offset in the buffer where the first byte should be read.</param>
    /// <param name="count">The maximum number of bytes that should be read.</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>How many bytes actually read, or 0 if end of file was already reached.</returns>
    public override Task<int> ReadAsync(
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken
    ) => Read(async: true, buffer, offset, count, cancellationToken);

    async Task<int> Read(
        bool async,
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(buffer);
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        if (buffer.Length - offset < count)
            throw new ArgumentException("Invalid offset or count for this buffer");

        CheckDisposed();

        var chunkCount = Math.Min(count, _manager.MaxTransferBlockSize);
        var read = 0;

        while (read < count)
        {
            var bytesRead = await _manager
                .ExecuteFunctionGetBytes(
                    async,
                    "loread",
                    buffer,
                    offset + read,
                    count - read,
                    cancellationToken,
                    _fd,
                    chunkCount
                )
                .ConfigureAwait(false);
            _pos += bytesRead;
            read += bytesRead;
            if (bytesRead < chunkCount)
            {
                return read;
            }
        }
        return read;
    }

    /// <summary>
    /// Writes <i>count</i> bytes to the large object.
    /// </summary>
    /// <param name="buffer">The buffer to write data from.</param>
    /// <param name="offset">The offset in the buffer at which to begin copying bytes.</param>
    /// <param name="count">The number of bytes to write.</param>
    public override void Write(byte[] buffer, int offset, int count) =>
        Write(async: false, buffer, offset, count).GetAwaiter().GetResult();

    /// <summary>
    /// Writes <i>count</i> bytes to the large object.
    /// </summary>
    /// <param name="buffer">The buffer to write data from.</param>
    /// <param name="offset">The offset in the buffer at which to begin copying bytes.</param>
    /// <param name="count">The number of bytes to write.</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    public override Task WriteAsync(
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken
    ) => Write(async: true, buffer, offset, count, cancellationToken);

    async Task Write(
        bool async,
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(buffer);
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        if (buffer.Length - offset < count)
            throw new ArgumentException("Invalid offset or count for this buffer");

        CheckDisposed();

        if (!_writeable)
            throw new NotSupportedException(
                "Write cannot be called on a stream opened with no write permissions"
            );

        var totalWritten = 0;

        while (totalWritten < count)
        {
            var chunkSize = Math.Min(count - totalWritten, _manager.MaxTransferBlockSize);
            var buf = new ArraySegment<byte>(buffer, offset + totalWritten, chunkSize);
            var bytesWritten = await _manager
                .ExecuteFunction<int>(async, "lowrite", cancellationToken, _fd, buf)
                .ConfigureAwait(false);
            totalWritten += bytesWritten;

            if (bytesWritten != chunkSize)
                throw new InvalidOperationException($"Internal Npgsql bug, please report");

            _pos += bytesWritten;
        }
    }

    /// <summary>
    /// CanTimeout always returns false.
    /// </summary>
    public override bool CanTimeout => false;

    /// <summary>
    /// CanRead always returns true, unless the stream has been closed.
    /// </summary>
    public override bool CanRead => !_disposed;

    /// <summary>
    /// CanWrite returns true if the stream was opened with write permissions, and the stream has not been closed.
    /// </summary>
    public override bool CanWrite => _writeable && !_disposed;

    /// <summary>
    /// CanSeek always returns true, unless the stream has been closed.
    /// </summary>
    public override bool CanSeek => !_disposed;

    /// <summary>
    /// Returns the current position in the stream. Getting the current position does not need a round-trip to the server, however setting the current position does.
    /// </summary>
    public override long Position
    {
        get
        {
            CheckDisposed();
            return _pos;
        }
        set => Seek(value, SeekOrigin.Begin);
    }

    /// <summary>
    /// Gets the length of the large object. This internally seeks to the end of the stream to retrieve the length, and then back again.
    /// </summary>
    public override long Length => GetLength(false).GetAwaiter().GetResult();

    /// <summary>
    /// Gets the length of the large object. This internally seeks to the end of the stream to retrieve the length, and then back again.
    /// </summary>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    public Task<long> GetLengthAsync(CancellationToken cancellationToken = default) =>
        GetLength(async: true);

    async Task<long> GetLength(bool async)
    {
        CheckDisposed();
        var old = _pos;
        var retval = await Seek(async, 0, SeekOrigin.End).ConfigureAwait(false);
        if (retval != old)
            await Seek(async, old, SeekOrigin.Begin).ConfigureAwait(false);
        return retval;
    }

    /// <summary>
    /// Seeks in the stream to the specified position. This requires a round-trip to the backend.
    /// </summary>
    /// <param name="offset">A byte offset relative to the <i>origin</i> parameter.</param>
    /// <param name="origin">A value of type SeekOrigin indicating the reference point used to obtain the new position.</param>
    /// <returns></returns>
    public override long Seek(long offset, SeekOrigin origin) =>
        Seek(async: false, offset, origin).GetAwaiter().GetResult();

    /// <summary>
    /// Seeks in the stream to the specified position. This requires a round-trip to the backend.
    /// </summary>
    /// <param name="offset">A byte offset relative to the <i>origin</i> parameter.</param>
    /// <param name="origin">A value of type SeekOrigin indicating the reference point used to obtain the new position.</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    public Task<long> SeekAsync(
        long offset,
        SeekOrigin origin,
        CancellationToken cancellationToken = default
    ) => Seek(async: true, offset, origin, cancellationToken);

    async Task<long> Seek(
        bool async,
        long offset,
        SeekOrigin origin,
        CancellationToken cancellationToken = default
    )
    {
        if (origin < SeekOrigin.Begin || origin > SeekOrigin.End)
            throw new ArgumentException("Invalid origin");
        if (!Has64BitSupport && offset != (int)offset)
            throw new ArgumentOutOfRangeException(
                nameof(offset),
                "offset must fit in 32 bits for PostgreSQL versions older than 9.3"
            );

        CheckDisposed();

        return _manager.Has64BitSupport
            ? _pos = await _manager
                .ExecuteFunction<long>(
                    async,
                    "lo_lseek64",
                    cancellationToken,
                    _fd,
                    offset,
                    (int)origin
                )
                .ConfigureAwait(false)
            : _pos = await _manager
                .ExecuteFunction<int>(
                    async,
                    "lo_lseek",
                    cancellationToken,
                    _fd,
                    (int)offset,
                    (int)origin
                )
                .ConfigureAwait(false);
    }

    /// <summary>
    /// Does nothing.
    /// </summary>
    public override void Flush() { }

    /// <summary>
    /// Truncates or enlarges the large object to the given size. If enlarging, the large object is extended with null bytes.
    /// For PostgreSQL versions earlier than 9.3, the value must fit in an Int32.
    /// </summary>
    /// <param name="value">Number of bytes to either truncate or enlarge the large object.</param>
    public override void SetLength(long value) =>
        SetLength(async: false, value).GetAwaiter().GetResult();

    /// <summary>
    /// Truncates or enlarges the large object to the given size. If enlarging, the large object is extended with null bytes.
    /// For PostgreSQL versions earlier than 9.3, the value must fit in an Int32.
    /// </summary>
    /// <param name="value">Number of bytes to either truncate or enlarge the large object.</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    public Task SetLength(long value, CancellationToken cancellationToken) =>
        SetLength(async: true, value, cancellationToken);

    async Task SetLength(bool async, long value, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        ArgumentOutOfRangeException.ThrowIfNegative(value);
        if (!Has64BitSupport && value != (int)value)
            throw new ArgumentOutOfRangeException(
                nameof(value),
                "offset must fit in 32 bits for PostgreSQL versions older than 9.3"
            );

        CheckDisposed();

        if (!_writeable)
            throw new NotSupportedException(
                "SetLength cannot be called on a stream opened with no write permissions"
            );

        if (_manager.Has64BitSupport)
            await _manager
                .ExecuteFunction<int>(async, "lo_truncate64", cancellationToken, _fd, value)
                .ConfigureAwait(false);
        else
            await _manager
                .ExecuteFunction<int>(async, "lo_truncate", cancellationToken, _fd, (int)value)
                .ConfigureAwait(false);
    }

    /// <summary>
    /// Releases resources at the backend allocated for this stream.
    /// </summary>
    public override void Close()
    {
        if (!_disposed)
        {
            _manager
                .ExecuteFunction<int>(async: false, "lo_close", CancellationToken.None, _fd)
                .GetAwaiter()
                .GetResult();
            _disposed = true;
        }
    }

    /// <summary>
    /// Releases resources at the backend allocated for this stream, iff disposing is true.
    /// </summary>
    /// <param name="disposing">Whether to release resources allocated at the backend.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Close();
        }
    }
}

/// <summary>
/// Large object manager. This class can be used to store very large files in a PostgreSQL database.
/// </summary>
/// <remarks>
/// Creates an NpgsqlLargeObjectManager for this connection. The connection must be opened to perform remote operations.
/// </remarks>
/// <param name="connection"></param>
public class NpgsqlLargeObjectManager(NpgsqlConnection connection)
{
    const int InvWrite = 0x00020000;
    const int InvRead = 0x00040000;

    internal NpgsqlConnection Connection { get; } = connection;

    /// <summary>
    /// The largest chunk size (in bytes) read and write operations will read/write each roundtrip to the network. Default 4 MB.
    /// </summary>
    public int MaxTransferBlockSize { get; set; } = 4 * 1024 * 1024; // 4MB

    /// <summary>
    /// Execute a function
    /// </summary>
    internal async Task<T> ExecuteFunction<T>(
        bool async,
        string function,
        CancellationToken cancellationToken,
        params object[] arguments
    )
    {
        using var command = Connection.CreateCommand();
        var stringBuilder = new StringBuilder("SELECT * FROM ").Append(function).Append('(');

        for (var i = 0; i < arguments.Length; i++)
        {
            if (i > 0)
                stringBuilder.Append(", ");
            stringBuilder.Append('$').Append(i + 1);
            command.Parameters.Add(new NpgsqlParameter { Value = arguments[i] });
        }

        stringBuilder.Append(')');
        command.CommandText = stringBuilder.ToString();

        return (T)
            (
                async
                    ? await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false)
                    : command.ExecuteScalar()
            )!;
    }

    /// <summary>
    /// Execute a function that returns a byte array
    /// </summary>
    /// <returns></returns>
    internal async Task<int> ExecuteFunctionGetBytes(
        bool async,
        string function,
        byte[] buffer,
        int offset,
        int len,
        CancellationToken cancellationToken,
        params object[] arguments
    )
    {
        using var command = Connection.CreateCommand();
        var stringBuilder = new StringBuilder("SELECT * FROM ").Append(function).Append('(');

        for (var i = 0; i < arguments.Length; i++)
        {
            if (i > 0)
                stringBuilder.Append(", ");
            stringBuilder.Append('$').Append(i + 1);
            command.Parameters.Add(new NpgsqlParameter { Value = arguments[i] });
        }

        stringBuilder.Append(')');
        command.CommandText = stringBuilder.ToString();

        var reader = async
            ? await command
                .ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken)
                .ConfigureAwait(false)
            : command.ExecuteReader(CommandBehavior.SequentialAccess);
        try
        {
            if (async)
                await reader.ReadAsync(cancellationToken).ConfigureAwait(false);
            else
                reader.Read();

            return (int)reader.GetBytes(0, 0, buffer, offset, len);
        }
        finally
        {
            if (async)
                await reader.DisposeAsync().ConfigureAwait(false);
            else
                reader.Dispose();
        }
    }

    /// <summary>
    /// Create an empty large object in the database. If an oid is specified but is already in use, an PostgresException will be thrown.
    /// </summary>
    /// <param name="preferredOid">A preferred oid, or specify 0 if one should be automatically assigned</param>
    /// <returns>The oid for the large object created</returns>
    /// <exception cref="PostgresException">If an oid is already in use</exception>
    public uint Create(uint preferredOid = 0) =>
        Create(preferredOid, false).GetAwaiter().GetResult();

    // Review unused parameters
    /// <summary>
    /// Create an empty large object in the database. If an oid is specified but is already in use, an PostgresException will be thrown.
    /// </summary>
    /// <param name="preferredOid">A preferred oid, or specify 0 if one should be automatically assigned</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>The oid for the large object created</returns>
    /// <exception cref="PostgresException">If an oid is already in use</exception>
    public Task<uint> CreateAsync(
        uint preferredOid,
        CancellationToken cancellationToken = default
    ) => Create(preferredOid, true, cancellationToken);

    Task<uint> Create(
        uint preferredOid,
        bool async,
        CancellationToken cancellationToken = default
    ) => ExecuteFunction<uint>(async, "lo_create", cancellationToken, (int)preferredOid);

    /// <summary>
    /// Opens a large object on the backend, returning a stream controlling this remote object.
    /// A transaction snapshot is taken by the backend when the object is opened with only read permissions.
    /// When reading from this object, the contents reflects the time when the snapshot was taken.
    /// Note that this method, as well as operations on the stream must be wrapped inside a transaction.
    /// </summary>
    /// <param name="oid">Oid of the object</param>
    /// <returns>An NpgsqlLargeObjectStream</returns>
    public NpgsqlLargeObjectStream OpenRead(uint oid) =>
        OpenRead(async: false, oid).GetAwaiter().GetResult();

    /// <summary>
    /// Opens a large object on the backend, returning a stream controlling this remote object.
    /// A transaction snapshot is taken by the backend when the object is opened with only read permissions.
    /// When reading from this object, the contents reflects the time when the snapshot was taken.
    /// Note that this method, as well as operations on the stream must be wrapped inside a transaction.
    /// </summary>
    /// <param name="oid">Oid of the object</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>An NpgsqlLargeObjectStream</returns>
    public Task<NpgsqlLargeObjectStream> OpenReadAsync(
        uint oid,
        CancellationToken cancellationToken = default
    ) => OpenRead(async: true, oid, cancellationToken);

    async Task<NpgsqlLargeObjectStream> OpenRead(
        bool async,
        uint oid,
        CancellationToken cancellationToken = default
    )
    {
        var fd = await ExecuteFunction<int>(async, "lo_open", cancellationToken, (int)oid, InvRead)
            .ConfigureAwait(false);
        return new NpgsqlLargeObjectStream(this, fd, false);
    }

    /// <summary>
    /// Opens a large object on the backend, returning a stream controlling this remote object.
    /// Note that this method, as well as operations on the stream must be wrapped inside a transaction.
    /// </summary>
    /// <param name="oid">Oid of the object</param>
    /// <returns>An NpgsqlLargeObjectStream</returns>
    public NpgsqlLargeObjectStream OpenReadWrite(uint oid) =>
        OpenReadWrite(async: false, oid).GetAwaiter().GetResult();

    /// <summary>
    /// Opens a large object on the backend, returning a stream controlling this remote object.
    /// Note that this method, as well as operations on the stream must be wrapped inside a transaction.
    /// </summary>
    /// <param name="oid">Oid of the object</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>An NpgsqlLargeObjectStream</returns>
    public Task<NpgsqlLargeObjectStream> OpenReadWriteAsync(
        uint oid,
        CancellationToken cancellationToken = default
    ) => OpenReadWrite(async: true, oid, cancellationToken);

    async Task<NpgsqlLargeObjectStream> OpenReadWrite(
        bool async,
        uint oid,
        CancellationToken cancellationToken = default
    )
    {
        var fd = await ExecuteFunction<int>(
                async,
                "lo_open",
                cancellationToken,
                (int)oid,
                InvRead | InvWrite
            )
            .ConfigureAwait(false);
        return new NpgsqlLargeObjectStream(this, fd, true);
    }

    /// <summary>
    /// Deletes a large object on the backend.
    /// </summary>
    /// <param name="oid">Oid of the object to delete</param>
    public void Unlink(uint oid) =>
        ExecuteFunction<object>(async: false, "lo_unlink", CancellationToken.None, (int)oid)
            .GetAwaiter()
            .GetResult();

    /// <summary>
    /// Deletes a large object on the backend.
    /// </summary>
    /// <param name="oid">Oid of the object to delete</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    public Task UnlinkAsync(uint oid, CancellationToken cancellationToken = default) =>
        ExecuteFunction<object>(async: true, "lo_unlink", cancellationToken, (int)oid);

    /// <summary>
    /// Exports a large object stored in the database to a file on the backend. This requires superuser permissions.
    /// </summary>
    /// <param name="oid">Oid of the object to export</param>
    /// <param name="path">Path to write the file on the backend</param>
    public void ExportRemote(uint oid, string path) =>
        ExecuteFunction<object>(async: false, "lo_export", CancellationToken.None, (int)oid, path)
            .GetAwaiter()
            .GetResult();

    /// <summary>
    /// Exports a large object stored in the database to a file on the backend. This requires superuser permissions.
    /// </summary>
    /// <param name="oid">Oid of the object to export</param>
    /// <param name="path">Path to write the file on the backend</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    public Task ExportRemoteAsync(
        uint oid,
        string path,
        CancellationToken cancellationToken = default
    ) => ExecuteFunction<object>(async: true, "lo_export", cancellationToken, (int)oid, path);

    /// <summary>
    /// Imports a large object to be stored as a large object in the database from a file stored on the backend. This requires superuser permissions.
    /// </summary>
    /// <param name="path">Path to read the file on the backend</param>
    /// <param name="oid">A preferred oid, or specify 0 if one should be automatically assigned</param>
    public uint ImportRemote(string path, uint oid = 0) =>
        ExecuteFunction<uint>(async: false, "lo_import", CancellationToken.None, path, (int)oid)
            .GetAwaiter()
            .GetResult();

    /// <summary>
    /// Imports a large object to be stored as a large object in the database from a file stored on the backend. This requires superuser permissions.
    /// </summary>
    /// <param name="path">Path to read the file on the backend</param>
    /// <param name="oid">A preferred oid, or specify 0 if one should be automatically assigned</param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    public Task<uint> ImportRemoteAsync(
        string path,
        uint oid,
        CancellationToken cancellationToken = default
    ) => ExecuteFunction<uint>(async: true, "lo_import", cancellationToken, path, (int)oid);

    /// <summary>
    /// Since PostgreSQL 9.3, large objects larger than 2GB can be handled, up to 4TB.
    /// This property returns true whether the PostgreSQL version is >= 9.3.
    /// </summary>
    public bool Has64BitSupport => true;
}
