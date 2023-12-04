using CurrentAccount.Application.CurrentAccount.Services;
using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Infrastructure.Database.Context;
using CurrentAccount.Infrastructure.Database.Models.CurrentAccount;
using Microsoft.EntityFrameworkCore;

namespace CurrentAccount.Infrastructure.Database.Repository
{
	public class CurrentAccountRepository : ICurrentAccountRepository
	{
		private readonly CurrentAccountContext _dbContext;
		private readonly ICurrentAccountInfraFactory _currentAccountInfraFactory;

		public CurrentAccountRepository(ICurrentAccountInfraFactory currentAccountInfraFactory, CurrentAccountContext currentAccountContext)
		{
			_dbContext = currentAccountContext;
			_currentAccountInfraFactory = currentAccountInfraFactory;
		}

		public Task<Guid> CreateCurrentAccount(CurrentAccountEntity currentAccountEntity)
		{
			var accountUuid = Guid.NewGuid();

			var currentAccount = new CurrentAccountDataModel
			{
				Id = accountUuid,
				AccountDigit = Convert.ToByte(currentAccountEntity.AccountDigit.AccountDigit),
				AccountHolderAddressId = currentAccountEntity.AccountHolderAddress.Id,
				AccountNumber = currentAccountEntity.AccountNumber.AccountNumber,
				AccountType = Enum.GetName(currentAccountEntity.AccountType),
				IsActive = true,
				Balance = 0,
				ContactInfoId = currentAccountEntity.ContactInfo.Id,
				CreationDate = DateTime.Now.ToUniversalTime(),
				Currency = currentAccountEntity.Currency.Currency,
				CustomerId = currentAccountEntity.Customer.CustomerId,
			};

			_dbContext.Add(currentAccount);
			_dbContext.SaveChanges();

			return Task.FromResult(accountUuid);
		}

		public Task<ResultModel<CurrentAccountEntity>> GetLastActiveAccountFromCustomer(CustomerEntity customer)
		{
			var currentAccount = _dbContext.CurrentAccounts
				.Include(x => x.ContactInfo)
				.Include(x => x.AccountHolderAddress)
				.OrderByDescending(x => x.CreationDate)
				.FirstOrDefault(x => x.Customer.CustomerId.Equals(customer.CustomerId));

			var entity = _currentAccountInfraFactory.ToCurrentAccountEntity(currentAccount);

			return Task.FromResult(entity);
		}

		public Task<string> GetLastCreatedAccountNumber()
		{
			var accountNumber = _dbContext.CurrentAccounts.OrderByDescending(x => x.CreationDate).Select(x => new { x.AccountNumber }).FirstOrDefault();

			var result = accountNumber == null ? string.Empty : accountNumber.AccountNumber;

			return Task.FromResult(result);
		}
	}
}
