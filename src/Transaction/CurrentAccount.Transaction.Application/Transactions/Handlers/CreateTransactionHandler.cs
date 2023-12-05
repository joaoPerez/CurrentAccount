using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Application.Transactions.Commands;
using CurrentAccount.Transaction.Core.Transactions;

namespace CurrentAccount.Transaction.Application.Transactions.Handlers
{
	public class CreateTransactionHandler : ICreateTransactionHandler
	{
		private readonly ITransactionService _transactionService;

        public CreateTransactionHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public Task<ResultModel<Guid>> HandleCreateTransaction(CreateTransactionCommand command)
		{
			throw new NotImplementedException();
		}
	}
}
