using CurrentAccount.Application.CurrentAccounts.Response;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Grpc.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CurrentAccount.Application.Transactions
{
	public class TransactionFromAccountGrpcService : ITransactionFromAccountGrpcService
	{
		private readonly AccountTransactionGrpcService.AccountTransactionGrpcServiceClient _serviceGrpcClient;

        public TransactionFromAccountGrpcService(AccountTransactionGrpcService.AccountTransactionGrpcServiceClient serviceGrpcClient)
        {
            _serviceGrpcClient = serviceGrpcClient;
        }

        public async Task<ResultModel<List<TransactionResponseModel>>> GetTransactionsFromAccount(Guid accountId)
        {
            var transactionsRequest = new AccountRequest { AccountId = accountId.ToString() };

            try
            {
				var transactions = await _serviceGrpcClient.GetTransactionsAsync(transactionsRequest);

                var transactionResponseModelList = new List<TransactionResponseModel>();

				foreach (var transaction in transactions.Transactions) 
                {
                    var transactionModel = new TransactionResponseModel(
                        Guid.Parse(transaction.TransactionId),
                        Guid.Parse(transaction.AccountId),
                        DateTime.Parse(transaction.TransactionDate),
                        transaction.Type,
                        Convert.ToDecimal(transaction.Amount),
                        transaction.Description,
                        Convert.ToDecimal(transaction.ActualBalance),
                        transaction.Currency);

                    transactionResponseModelList.Add(transactionModel);
                }

                return ResultModel<List<TransactionResponseModel>>.Success(transactionResponseModelList);
			}
			catch (RpcException ex) 
            {
                return ResultModel<List<TransactionResponseModel>>.Failure(ex.Message);
            }
		}
    }
}
