using Domain.User;

namespace Storage;

public sealed record AccessControl(long ResourceId, UserId[] Users, AccessControlLevel Level) { }

public enum AccessControlLevel : byte
{
    Public = 0,
    Auth = 1,
    Private = 2,
}
