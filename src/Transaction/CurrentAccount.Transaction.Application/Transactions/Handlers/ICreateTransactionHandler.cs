using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Core.Shared.Transactions.Commands;

namespace CurrentAccount.Transaction.Application.Transactions.Handlers
{
	public interface ICreateTransactionHandler
	{
		Task<ResultModel<Guid>> HandleCreateTransaction(CreateTransactionCommand command);
	}
}
