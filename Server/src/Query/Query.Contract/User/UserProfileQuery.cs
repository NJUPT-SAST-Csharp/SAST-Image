using Domain.User;
using Mediator;

namespace Query.User;

[Alias(nameof(UserProfileDto))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed record class UserProfileDto(
    long Id,
    string Username,
    string Nickname,
    string Biography
);

[Alias(nameof(UserProfileQuery))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed record UserProfileQuery(UserId User) : IQuery<UserProfileDto?>;
