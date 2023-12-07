
using CurrentAccount.Transaction.Application.Transactions.Handlers;
using CurrentAccount.Transaction.Grpc.Protos;
using Grpc.Core;

namespace CurrentAccount.Transaction.Grpc.Services
{
	public class AccountTransactionService : AccountTransactionGrpcService.AccountTransactionGrpcServiceBase
	{
		private readonly string _invalidParameterErrorMessage = "Invalid parameter";
		private readonly string _internalErrorMessage = "Internal error";

		private readonly IGetAccountTransactionsHandler _getAccountTransactionsHandler;

		public AccountTransactionService(IGetAccountTransactionsHandler getAccountTransactionsHandler)
		{
			_getAccountTransactionsHandler = getAccountTransactionsHandler;
		}

		public override async Task<TransactionListResponse> GetTransactions(AccountRequest request, ServerCallContext context)
		{
			var isValidAccountId = Guid.TryParse(request.AccountId, out var accountId);

			if (!isValidAccountId)
			{
				//_logger.LogError(_invalidParameterErrorMessage, request.AccountId);
				throw new RpcException(new Status(StatusCode.InvalidArgument, _invalidParameterErrorMessage));
			}

			var transactionEntities = await _getAccountTransactionsHandler.HandleTransactionsFromAccount(accountId);

			if (!transactionEntities.IsSuccess)
			{
				throw new RpcException(new Status(StatusCode.Internal, _internalErrorMessage));
			}

			var transactionListResponse = new TransactionListResponse();

			if (transactionEntities.Value != null && transactionEntities.Value.Count > 0)
			{
				foreach (var transaction in transactionEntities.Value)
				{
					var transactionResponse = new TransactionResponse
					{
						AccountId = transaction.AccountId.ToString(),
						ActualBalance = Convert.ToDouble(transaction.ActualBalance.Value),
						Amount = Convert.ToDouble(transaction.Amount.Value),
						Currency = transaction.Currency.Currency,
						Description = transaction.Description.Name,
						TransactionDate = transaction.TransactionDate.RecordedDate.ToString(),
						TransactionId = transaction.TransactionId.ToString(),
						Type = Enum.GetName(transaction.Type)
					};

					transactionListResponse.Transactions.Add(transactionResponse);
				}
			}

			return transactionListResponse;
		}
	}
}
