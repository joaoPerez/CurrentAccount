using CurrentAccount.Core.Shared;

namespace CurrentAccount.Core.CurrentAccount
{
    public record AccountHolderAddressValue(NameWithNumValue Street, NameValue City, NameValue State, ZipCodeValue ZipCode, NameValue Country);
}
