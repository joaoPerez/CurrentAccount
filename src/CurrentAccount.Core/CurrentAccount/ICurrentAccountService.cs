using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Core.CurrentAccount
{
	public interface ICurrentAccountService
	{
		Task<ResultModel<Guid>> CreateNewAccountFromExistentCostumer(Guid customerId, decimal initialCredit);
	}
}
