using CurrentAccount.Core.Shared;

namespace CurrentAccount.Core.CurrentAccount
{
    public record ContactInformationValue(Guid Id, PhoneNumberValue PhoneNumber, EmailAddressValue Email);
}