using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;

namespace CurrentAccount.Infrastructure.Repository
{
	public class CurrentAccountRepository : ICurrentAccountRepository
	{
		public Task<Guid> CreateCurrentAccount(CurrentAccountEntity currentAccountEntity)
		{
			throw new NotImplementedException();
		}

		public Task<CurrentAccountEntity> GetLastActiveAccountFromCustomer(CustomerEntity customer)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetLastCreatedAccountNumber()
		{
			throw new NotImplementedException();
		}
	}
}
