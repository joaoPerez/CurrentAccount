using CurrentAccount.Core.Shared;

namespace CurrentAccount.Core.CurrentAccount
{
    public record AccountHolderAddressValue(NameValue Street, NameValue City, NameValue State, ZipCodeValue ZipCode, NameValue Country);
}
