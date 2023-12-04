using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Core.Shared;
using CurrentAccount.Infrastructure.Database.Models.Customer;
using CurrentAccount.Infrastructure.Database.Models.CurrentAccount;

namespace CurrentAccount.Application.CurrentAccount.Services
{
    public class CurrentAccountInfraFactory : ICurrentAccountInfraFactory
    {
		public ResultModel<CurrentAccountEntity> ToCurrentAccountEntity(CurrentAccountDataModel accountDataModel)
		{
			// I'm not applying the business account logic here also because it should be moved to a CustomerFactory.
			var individualCustomer = (accountDataModel.Customer as IndividualCustomerDataModel);
			
			var firstNameValue = NameValue.Create(individualCustomer.FirstName);

			if (!firstNameValue.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(firstNameValue.ErrorMessage);
			}

			var lastNameValue = NameValue.Create(individualCustomer.LastName);

			if (!lastNameValue.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(lastNameValue.ErrorMessage);
			}

			var customerEntity = new IndividualCustomerEntity(individualCustomer.CustomerId, firstNameValue.Value, firstNameValue.Value);
			
			// Map to CurrentAccountEntity
			var accountNumberResult = AccountNumberValue.Create(accountDataModel.AccountNumber);
			if (!accountNumberResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(accountNumberResult.ErrorMessage);
			}

			var accountDigitResult = AccountDigitValue.Create(accountDataModel.AccountDigit);
			if (!accountDigitResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(accountDigitResult.ErrorMessage);
			}

			var balanceResult = DecimalNumberValue.Create(accountDataModel.Balance);
			if (!balanceResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(balanceResult.ErrorMessage);
			}

			var currencyResult = CurrencyValue.Create(accountDataModel.Currency);
			if (!currencyResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(currencyResult.ErrorMessage);
			}

			var streetResult = NameWithNumValue.Create(accountDataModel.AccountHolderAddress.Street);
			if (!streetResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(streetResult.ErrorMessage);
			}

			var cityResult = NameValue.Create(accountDataModel.AccountHolderAddress.City);
			if (!cityResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(cityResult.ErrorMessage);
			}

			var stateResult = NameValue.Create(accountDataModel.AccountHolderAddress.State);
			if (!stateResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(stateResult.ErrorMessage);
			}

			var zipCodeResult = ZipCodeValue.Create(accountDataModel.AccountHolderAddress.ZipCode);
			if (!zipCodeResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(zipCodeResult.ErrorMessage);
			}

			var countryResult = NameValue.Create(accountDataModel.AccountHolderAddress.Country);
			if (!countryResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(countryResult.ErrorMessage);
			}

			var phoneNumberResult = PhoneNumberValue.Create(accountDataModel.ContactInfo.PhoneNumber, accountDataModel.ContactInfo.CountryCode);
			if (!phoneNumberResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(phoneNumberResult.ErrorMessage);
			}

			var emailResult = EmailAddressValue.Create(accountDataModel.ContactInfo.Email);
			if (!emailResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(emailResult.ErrorMessage);
			}

			var creationDateResult = RecordedDateValue.Create(accountDataModel.CreationDate);
			if (!creationDateResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(creationDateResult.ErrorMessage);
			}

			// Checking if the closing date has value
			var closingDateResult = accountDataModel.ClosingDate != default
				? RecordedDateValue.Create(accountDataModel.ClosingDate.Value)
				: ResultModel<RecordedDateValue>.Success(null);

			if (!closingDateResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(closingDateResult.ErrorMessage);
			}

			var currentAccountEntity = new CurrentAccountEntity(
				accountId: accountDataModel.Id,
				accountNumber: accountNumberResult.Value,
				accountDigit: accountDigitResult.Value,
				customer: customerEntity,
				accountType: Enum.GetName(AccountTypeEnum.Individual).Equals(accountDataModel.AccountType) ? AccountTypeEnum.Individual : AccountTypeEnum.Business,
				balance: balanceResult.Value,
				currency: currencyResult.Value,
				accountHolderAddress: new AccountHolderAddressValue(
					Id: accountDataModel.AccountHolderAddress.Id,
					Street: streetResult.Value,
					City: cityResult.Value,
					State: stateResult.Value,
					ZipCode: zipCodeResult.Value,
					Country: countryResult.Value
				),
				contactInfo: new ContactInformationValue(
					Id: accountDataModel.ContactInfo.Id,
					PhoneNumber: phoneNumberResult.Value,
					Email: emailResult.Value
				),
				isActive: accountDataModel.IsActive,
				creationDate: creationDateResult.Value,
				closingDate: closingDateResult.Value
			);

			return ResultModel<CurrentAccountEntity>.Success(currentAccountEntity);
		}
	}
}
