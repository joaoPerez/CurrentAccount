using CurrentAccount.Core.Customer;

namespace CurrentAccount.Core.CurrentAccount
{
	public interface ICurrentAccountRepository
	{
		Task<Guid> CreateCurrentAccount(CurrentAccountEntity currentAccountEntity);
		Task<string> GetLastCreatedAccountNumber();
		Task<CurrentAccountEntity> GetLastActiveAccountFromCustomer(CustomerEntity customer);
	}
}
