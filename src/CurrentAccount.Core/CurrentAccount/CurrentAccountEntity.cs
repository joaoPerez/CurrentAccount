using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared;

namespace CurrentAccount.Core.CurrentAccount
{
    public class CurrentAccountEntity
	{
		public CurrentAccountEntity(Guid accountId,
									AccountNumberValue accountNumber,
									AccountDigitValue accountDigit,
									CustomerEntity customer,
									AccountTypeEnum accountType,
									DecimalNumberValue balance,
									CurrencyValue currency,
									AccountHolderAddressValue accountHolderAddress,
									ContactInformationValue contactInfo,
									bool isActive,
									RecordedDateValue creationDate,
									#nullable enable
									RecordedDateValue? closingDate)
		{
			AccountId = accountId;
			AccountNumber = accountNumber;
			AccountDigit = accountDigit;
			Customer = customer;
			AccountType = accountType;
			Balance = balance;
			Currency = currency;
			AccountHolderAddress = accountHolderAddress;
			ContactInfo = contactInfo;
			IsActive = isActive;
			CreationDate = creationDate;
			ClosingDate = closingDate;
		}

		public Guid AccountId { get; private set; }
		public AccountNumberValue AccountNumber { get; private set; }
		public AccountDigitValue AccountDigit { get; private set; }
		public CustomerEntity Customer { get; private set; }
		public AccountTypeEnum AccountType { get; private set; }
		public DecimalNumberValue Balance { get; private set; }
		public CurrencyValue Currency { get; private set; }
		public AccountHolderAddressValue AccountHolderAddress { get; private set; }
		public ContactInformationValue ContactInfo { get; private set; }
		public bool IsActive { get; private set; }
		public RecordedDateValue CreationDate { get; private set; }
		#nullable enable
		public RecordedDateValue? ClosingDate { get; private set; }

		public void SetAccountNumber(AccountNumberValue accountNumber) { AccountNumber = accountNumber; } 
	}
}