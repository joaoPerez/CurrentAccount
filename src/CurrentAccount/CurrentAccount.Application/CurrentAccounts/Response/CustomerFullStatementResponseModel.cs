namespace CurrentAccount.Application.CurrentAccounts.Response
{
	public record CustomerFullStatementResponseModel(string Name, 
													 string Surname, 
													 decimal Balance,
													 List<TransactionResponseModel> Transactions
													 );
}
