using System.Diagnostics.CodeAnalysis;
using Domain.Entity;

namespace Domain.UserAggregate.UserEntity;

[Flags]
public enum Role : byte
{
    User = 1,
    Admin = User << 1,
}

public sealed class Roles() : ValueObjects<Roles, Role>, IFactoryConstructor<Roles, Role[]>
{
    internal Roles(params Role[] roles)
        : this() => Value = roles;

    public static bool TryCreateNew(
        Role[] input,
        [MaybeNullWhen(false), NotNullWhen(true)] out Roles? newObject
    )
    {
        newObject = default;
        if (input.Length == 0)
        {
            newObject = new();
            return true;
        }

        HashSet<Role> set = [];
        for (int i = 0; i < input.Length; i++)
        {
            if (Enum.IsDefined(input[i]) is false)
                return false;

            set.Add(input[i]);
        }

        newObject = new() { Value = [.. set] };
        return true;
    }
}
