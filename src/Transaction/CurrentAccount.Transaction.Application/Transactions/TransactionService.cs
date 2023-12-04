using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Core.Transactions;

namespace CurrentAccount.Transaction.Application.Transactions
{
	public class TransactionService : ITransactionService
	{
		public Task<Guid> CreateTransaction(TransactionEntity transaction)
		{
			throw new NotImplementedException();
		}

		public Task<ResultModel<List<TransactionEntity>>> GetAllTransactionsFromAccount(Guid accountId)
		{
			throw new NotImplementedException();
		}

		public Task<decimal> GetLastBalanceFromAccount(Guid accountId)
		{
			throw new NotImplementedException();
		}
	}
}
