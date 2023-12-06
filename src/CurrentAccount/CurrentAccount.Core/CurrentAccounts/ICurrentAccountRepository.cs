using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Core.CurrentAccount
{
	public interface ICurrentAccountRepository
	{
		Task<Guid> CreateCurrentAccount(CurrentAccountEntity currentAccountEntity);
		Task<string> GetLastCreatedAccountNumber();
		Task<ResultModel<CurrentAccountEntity>> GetLastActiveAccountFromCustomer(CustomerEntity customer);
		Task<List<Guid>> GetCurrentAccountsFromCustomer(Guid customerId);
	}
}
