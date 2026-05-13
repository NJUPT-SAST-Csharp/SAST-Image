using Domain.User;
using Mediator;

namespace Query.User;

[Alias(nameof(UsernameExistence))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public readonly record struct UsernameExistence(bool IsExist);

[Alias(nameof(UsernameExistenceQuery))]
[GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
public sealed record class UsernameExistenceQuery(Username Username) : IQuery<UsernameExistence>;
