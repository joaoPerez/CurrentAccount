using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared;

namespace CurrentAccount.Core.CurrentAccount
{
    public class CurrentAccountEntity
	{
		public CurrentAccountEntity(AccountNumberValue accountNumber,
									AccountDigitValue accountDigit,
									NameValue accountHolderName,
									CustomerEntity customer,
									AccountTypeEnum accountType,
									DecimalNumberValue balance,
									CurrencyValue currency,
									DecimalNumberValue overdraftLimit,
									DecimalNumberValue interestRate,
									DecimalNumberValue withdrawalLimit,
									AccountHolderAddressValue accountHolderAddress,
									ContactInformationValue contactInfo,
									bool isActive,
									RecordedDateValue creationDate,
									#nullable enable
									RecordedDateValue? closingDate)
		{
			AccountNumber = accountNumber;
			AccountDigit = accountDigit;
			AccountHolderName = accountHolderName;
			Customer = customer;
			AccountType = accountType;
			Balance = balance;
			Currency = currency;
			OverdraftLimit = overdraftLimit;
			InterestRate = interestRate;
			WithdrawalLimit = withdrawalLimit;
			AccountHolderAddress = accountHolderAddress;
			ContactInfo = contactInfo;
			IsActive = isActive;
			CreationDate = creationDate;
			ClosingDate = closingDate;
		}

		public AccountNumberValue AccountNumber { get; private set; }
		public AccountDigitValue AccountDigit { get; private set; }
		public NameValue AccountHolderName { get; private set; }
		public CustomerEntity Customer { get; private set; }
		public AccountTypeEnum AccountType { get; private set; }
		public DecimalNumberValue Balance { get; private set; }
		public CurrencyValue Currency { get; private set; }
		public DecimalNumberValue OverdraftLimit { get; private set; }
		public DecimalNumberValue InterestRate { get; private set; }
		public DecimalNumberValue WithdrawalLimit { get; private set; }
		public AccountHolderAddressValue AccountHolderAddress { get; private set; }
		public ContactInformationValue ContactInfo { get; private set; }
		public bool IsActive { get; private set; }
		public RecordedDateValue CreationDate { get; private set; }
		#nullable enable
		public RecordedDateValue? ClosingDate { get; private set; }

		public void SetAccountNumber(AccountNumberValue accountNumber) { AccountNumber = accountNumber; } 
	}
}