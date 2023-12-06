using CurrentAccount.Core.Shared.Transactions.Commands;

namespace CurrentAccount.Application.Transactions
{
	public interface ICreateTransactionHandler
	{
		Task HandleTransactionEvent(CreateTransactionCommand command);
	}
}
