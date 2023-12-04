using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Transaction.Core.Transactions
{
	public interface ITransactionService
	{
		Task<Guid> CreateTransaction(TransactionEntity transaction);
		Task<decimal> GetLastBalanceFromAccount(Guid accountId);
		Task<ResultModel<List<TransactionEntity>>> GetAllTransactionsFromAccount(Guid accountId);
	}
}
