namespace CurrentAccount.Transaction.Application.Transactions.Commands
{
	public record CreateTransactionCommand(Guid accountId,
										   string transactionType,
										   decimal amount,
										   string description,
										   string currency);
}
