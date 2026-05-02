namespace Domain.UserAggregate.Services;

public sealed class JwtAuthOptions()
{
    public required string SecKey { get; init; }
    public required string Algorithm { get; init; }
    public required int Expires { get; init; }
}
