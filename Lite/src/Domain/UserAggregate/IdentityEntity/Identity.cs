using System.Runtime.CompilerServices;
using Domain.Entity;

namespace Domain.UserAggregate.IdentityEntity;

public sealed class Identity : EntityBase<Guid>
{
    [Obsolete("For ORM", true)]
    private Identity()
        : base(default) { }

    private readonly IdentityProvider _provider;
    private readonly IdentityId _providerUserId;

    internal Identity(IdentityId providerUserId, IdentityProvider provider)
        : base(Guid.CreateVersion7())
    {
        _provider = provider;
        _providerUserId = providerUserId;
    }
}

internal static class IdentitiesExtensions
{
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_provider")]
    private static extern ref IdentityProvider GetProvider(this Identity identity);

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_providerUserId")]
    private static extern ref IdentityId GetProviderUserId(this Identity identity);

    extension<T>(T identities)
        where T : ICollection<Identity>
    {
        /// <summary>
        /// Determines whether the current user is authenticated with a GitHub identity provider.
        /// </summary>
        /// <remarks>
        /// Ensure call this method only after confirming that the user is authenticated, as it relies on the presence
        /// of identity information to determine the authentication status with GitHub.
        /// </remarks>
        /// <returns>true if the user has an identity associated with GitHub; otherwise, false.</returns>
        public bool LoginGitHub()
        {
            return identities.Any(identity => identity.GetProvider() == IdentityProvider.GitHub);
        }

        /// <summary>
        /// Links the specified GitHub identity to the current user, replacing any existing GitHub identity if present.
        /// </summary>
        /// <param name="gitHubId">The unique identifier of the GitHub identity to associate with the user.</param>
        public void LinkGitHub(IdentityId gitHubId)
        {
            var identity = identities.FirstOrDefault(i =>
                i.GetProvider() == IdentityProvider.GitHub
            );

            if (identity is null)
            {
                identities.Add(new(gitHubId, IdentityProvider.GitHub));
                return;
            }
            if (identity.GetProviderUserId() == gitHubId)
                return;

            identities.Remove(identity);
            identities.Add(new(gitHubId, IdentityProvider.GitHub));
        }
    }
}
