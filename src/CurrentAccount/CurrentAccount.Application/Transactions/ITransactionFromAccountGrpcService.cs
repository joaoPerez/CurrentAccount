using CurrentAccount.Application.CurrentAccounts.Response;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Application.Transactions
{
	public interface ITransactionFromAccountGrpcService
	{
		Task<ResultModel<List<TransactionResponseModel>>> GetTransactionsFromAccount(Guid accountId);
	}
}
