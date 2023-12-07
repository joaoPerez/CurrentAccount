using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Core.Transactions;

namespace CurrentAccount.Transaction.Application.Transactions.Handlers
{
	public interface IGetAccountTransactionsHandler
	{
		Task<ResultModel<List<TransactionEntity>>> HandleTransactionsFromAccount(Guid accountId);
	}
}
