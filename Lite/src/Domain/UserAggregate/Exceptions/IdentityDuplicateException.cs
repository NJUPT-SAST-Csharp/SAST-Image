using Domain.Extensions;
using Domain.UserAggregate.IdentityEntity;

namespace Domain.UserAggregate.Exceptions;

public sealed class IdentityDuplicateException(IdentityId id) : DomainException
{
    public IdentityId Id { get; } = id;
}
