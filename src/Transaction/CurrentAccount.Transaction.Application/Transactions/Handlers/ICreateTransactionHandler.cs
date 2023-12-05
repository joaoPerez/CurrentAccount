using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Application.Transactions.Commands;

namespace CurrentAccount.Transaction.Application.Transactions.Handlers
{
	public interface ICreateTransactionHandler
	{
		Task<ResultModel<Guid>> HandleCreateTransaction(CreateTransactionCommand command);
	}
}
