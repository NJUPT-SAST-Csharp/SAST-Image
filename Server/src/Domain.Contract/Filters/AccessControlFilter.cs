using System.Reflection;

namespace Domain.Filters;

/// <summary>
/// Specifies the required user role for accessing a method.
/// </summary>
/// <remarks>
/// Apply this attribute to methods to restrict access based on user roles. Methods without this
/// attribute are accessible to all roles by default. This attribute is typically used in authorization frameworks to
/// enforce role-based access control.
/// </remarks>
/// <param name="role">The minimum user role required to invoke the decorated method. Defaults to <see cref="Role.User"/>.</param>
[AttributeUsage(AttributeTargets.Method)]
internal sealed class AccessControlAttribute(Role role = Role.User) : Attribute
{
    public Role Role { get; } = role;
}

public sealed class AccessControlFilter : IOutgoingGrainCallFilter
{
    public Task Invoke(IOutgoingGrainCallContext context)
    {
        if (
            context.InterfaceMethod.GetCustomAttribute<AccessControlAttribute>() is not { } required
        )
            return context.Invoke();

        if (
            RequestContext.Get(nameof(Actor))
            is not Actor { Role: Role actorRole, Id: UserId actorId }
        )
            throw new ForbiddenException();

        if ((actorRole & required.Role) != required.Role)
            throw new ForbiddenException();

        return context.Invoke();
    }
}

[GenerateSerializer]
[Alias("403_exception")]
public sealed class ForbiddenException : Exception;
