using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Core.Transactions;

namespace CurrentAccount.Transaction.Application.Transactions.Handlers
{
	public class GetAccountTransactionsHandler : IGetAccountTransactionsHandler
	{
		private readonly ITransactionService _transactionService;

        public GetAccountTransactionsHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public Task<ResultModel<List<TransactionEntity>>> HandleTransactionsFromAccount(Guid accountId)
		{
			return _transactionService.GetTransactionsFromAccount(accountId);
		}
	}
}
