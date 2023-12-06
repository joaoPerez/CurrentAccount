namespace CurrentAccount.Core.Shared.Transactions.Commands
{
    public record CreateTransactionCommand(Guid accountId,
                                           string transactionType,
                                           decimal amount,
                                           string description,
                                           string currency);
}
