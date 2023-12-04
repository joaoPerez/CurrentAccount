using CurrentAccount.Core.Shared;

namespace CurrentAccount.Core.CurrentAccount
{
    public record AccountHolderAddressValue(Guid Id, NameWithNumValue Street, NameValue City, NameValue State, ZipCodeValue ZipCode, NameValue Country);
}
