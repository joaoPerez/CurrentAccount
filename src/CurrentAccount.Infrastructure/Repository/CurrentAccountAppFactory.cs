using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Core.Shared;

namespace CurrentAccount.Infrastructure.Repository
{
	public class CurrentAccountAppFactoryInfra
	{
		private readonly CurrentAccountDto _currentAccountDto;
		private readonly CustomerEntity _customerEntity;
		private readonly Guid _accountUuid;

		public CurrentAccountAppFactoryInfra(Guid accountUuid, CurrentAccountDto currentAccountDto, CustomerEntity customerEntity)
		{
			_currentAccountDto = currentAccountDto;
			_customerEntity = customerEntity;
			_accountUuid = accountUuid;
		}

		public ResultModel<CurrentAccountEntity> ToCurrentAccountEntity()
		{
			var customer = _customerEntity;
			var accountId = _accountUuid;

			// Map to CurrentAccountEntity
			var accountNumberResult = AccountNumberValue.Create(_currentAccountDto.AccountNumber);
			if (!accountNumberResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(accountNumberResult.ErrorMessage);
			}

			var accountDigitResult = AccountDigitValue.Create(_currentAccountDto.AccountDigit);
			if (!accountDigitResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(accountDigitResult.ErrorMessage);
			}

			var balanceResult = DecimalNumberValue.Create(_currentAccountDto.Balance);
			if (!balanceResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(balanceResult.ErrorMessage);
			}

			var currencyResult = CurrencyValue.Create(_currentAccountDto.Currency);
			if (!currencyResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(currencyResult.ErrorMessage);
			}

			var streetResult = NameWithNumValue.Create(_currentAccountDto.Street);
			if (!streetResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(streetResult.ErrorMessage);
			}

			var cityResult = NameValue.Create(_currentAccountDto.City);
			if (!cityResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(cityResult.ErrorMessage);
			}

			var stateResult = NameValue.Create(_currentAccountDto.State);
			if (!stateResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(stateResult.ErrorMessage);
			}

			var zipCodeResult = ZipCodeValue.Create(_currentAccountDto.ZipCode);
			if (!zipCodeResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(zipCodeResult.ErrorMessage);
			}

			var countryResult = NameValue.Create(_currentAccountDto.Country);
			if (!countryResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(countryResult.ErrorMessage);
			}

			var phoneNumberResult = PhoneNumberValue.Create(_currentAccountDto.PhoneNumber, _currentAccountDto.CountryCode);
			if (!phoneNumberResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(phoneNumberResult.ErrorMessage);
			}

			var emailResult = EmailAddressValue.Create(_currentAccountDto.Email);
			if (!emailResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(emailResult.ErrorMessage);
			}

			var creationDateResult = RecordedDateValue.Create(_currentAccountDto.CreationDate);
			if (!creationDateResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(creationDateResult.ErrorMessage);
			}

			// Checking if the closing date has value
			var closingDateResult = _currentAccountDto.ClosingDate != default
				? RecordedDateValue.Create(_currentAccountDto.ClosingDate.Value)
				: ResultModel<RecordedDateValue>.Success(null);

			if (!closingDateResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(closingDateResult.ErrorMessage);
			}

			var currentAccountEntity = new CurrentAccountEntity(
				accountId: accountId,
				accountNumber: accountNumberResult.Value,
				accountDigit: accountDigitResult.Value,
				customer: customer,
				accountType: _currentAccountDto.AccountType,
				balance: balanceResult.Value,
				currency: currencyResult.Value,
				accountHolderAddress: new AccountHolderAddressValue(
					Street: streetResult.Value,
					City: cityResult.Value,
					State: stateResult.Value,
					ZipCode: zipCodeResult.Value,
					Country: countryResult.Value
				),
				contactInfo: new ContactInformationValue(
					PhoneNumber: phoneNumberResult.Value,
					Email: emailResult.Value
				),
				isActive: _currentAccountDto.IsActive,
				creationDate: creationDateResult.Value,
				closingDate: closingDateResult.Value
			);

			return ResultModel<CurrentAccountEntity>.Success(currentAccountEntity);
		}
	}
}
