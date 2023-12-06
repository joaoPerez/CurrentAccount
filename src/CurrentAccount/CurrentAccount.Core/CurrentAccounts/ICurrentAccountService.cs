using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Core.CurrentAccount
{
	public interface ICurrentAccountService
	{
		Task<Guid> CreateCurrentAccount(CurrentAccountEntity currentAccount);
		Task<ResultModel<CurrentAccountEntity>> GetLastActiveAccountFromCustomer(CustomerEntity customer);
		Task<string> CalculateNextAccountNumber();
		Task<List<Guid>> GetCurrentAccountsFromCustomer(Guid customerId);
	}
}
