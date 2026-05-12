namespace Storage;

public sealed class StorageOptions
{
    public required Uri BaseUri { get; init; }
    public required int Quality { get; init; } = 75;
    public required int Effort { get; init; } = 0;
}
