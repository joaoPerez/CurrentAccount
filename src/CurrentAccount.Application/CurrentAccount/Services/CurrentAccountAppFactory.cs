﻿using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Core.Shared;
using CurrentAccount.Application.CurrentAccount.Dto;

namespace CurrentAccount.Application.CurrentAccount.Services
{
    public class CurrentAccountAppFactory : ICurrentAccountAppFactory
    {
		public ResultModel<CurrentAccountEntity> ToCurrentAccountEntity(Guid accountUuid, CurrentAccountDto currentAccountDto, CustomerEntity customerEntity)
		{
			var customer = customerEntity;
			var accountId = accountUuid;

			// Map to CurrentAccountEntity
			var accountNumberResult = AccountNumberValue.Create(currentAccountDto.AccountNumber);
			if (!accountNumberResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(accountNumberResult.ErrorMessage);
			}

			var accountDigitResult = AccountDigitValue.Create(currentAccountDto.AccountDigit);
			if (!accountDigitResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(accountDigitResult.ErrorMessage);
			}

			var balanceResult = DecimalNumberValue.Create(currentAccountDto.Balance);
			if (!balanceResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(balanceResult.ErrorMessage);
			}

			var currencyResult = CurrencyValue.Create(currentAccountDto.Currency);
			if (!currencyResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(currencyResult.ErrorMessage);
			}

			var streetResult = NameWithNumValue.Create(currentAccountDto.Street);
			if (!streetResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(streetResult.ErrorMessage);
			}

			var cityResult = NameValue.Create(currentAccountDto.City);
			if (!cityResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(cityResult.ErrorMessage);
			}

			var stateResult = NameValue.Create(currentAccountDto.State);
			if (!stateResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(stateResult.ErrorMessage);
			}

			var zipCodeResult = ZipCodeValue.Create(currentAccountDto.ZipCode);
			if (!zipCodeResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(zipCodeResult.ErrorMessage);
			}

			var countryResult = NameValue.Create(currentAccountDto.Country);
			if (!countryResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(countryResult.ErrorMessage);
			}

			var phoneNumberResult = PhoneNumberValue.Create(currentAccountDto.PhoneNumber, currentAccountDto.CountryCode);
			if (!phoneNumberResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(phoneNumberResult.ErrorMessage);
			}

			var emailResult = EmailAddressValue.Create(currentAccountDto.Email);
			if (!emailResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(emailResult.ErrorMessage);
			}

			var creationDateResult = RecordedDateValue.Create(currentAccountDto.CreationDate);
			if (!creationDateResult.IsSuccess)
			{
				return ResultModel<CurrentAccountEntity>.Failure(creationDateResult.ErrorMessage);
			}

			// Checking if the closing date has value
			var closingDateResult = currentAccountDto.ClosingDate != default
				? RecordedDateValue.Create(currentAccountDto.ClosingDate.Value)
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
				accountType: currentAccountDto.AccountType,
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
				isActive: currentAccountDto.IsActive,
				creationDate: creationDateResult.Value,
				closingDate: closingDateResult.Value
			);

			return ResultModel<CurrentAccountEntity>.Success(currentAccountEntity);
		}
	}
}