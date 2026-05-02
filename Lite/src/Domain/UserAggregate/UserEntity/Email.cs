using System.Diagnostics.CodeAnalysis;
using Domain.Entity;
using Domain.Shared.Converter;

namespace Domain.UserAggregate.UserEntity;

[OpenJsonConverter<Email, string>]
public readonly record struct Email(string Value)
    : IValueObject<Email, string>,
        IFactoryConstructor<Email, string>
{
    public static bool TryCreateNew(string input, [NotNullWhen(true)] out Email newObject)
    {
        if (
            string.IsNullOrWhiteSpace(input)
            || System.Net.Mail.MailAddress.TryCreate(input, out var mailAddress) is false
        )
        {
            newObject = default;
            return false;
        }

        newObject = new Email(mailAddress.Address);
        return true;
    }
}
