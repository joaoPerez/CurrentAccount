﻿using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Core.Transactions;

namespace CurrentAccount.Transaction.Application.Transactions.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ResultModel<Guid>> CreateTransaction(TransactionEntity transaction)
        {
            var _createTransactionErrorMessage = "The transaction could not be created";
            var result = await _transactionRepository.CreateTransaction(transaction);

            if(result.Equals(Guid.Empty)) { return ResultModel<Guid>.Failure(_createTransactionErrorMessage); }

            return ResultModel<Guid>.Success(result);
        }

        public Task<ResultModel<List<TransactionEntity>>> GetAllTransactionsFromAccount(Guid accountId)
        {
            return _transactionRepository.GetAllTransactionsFromAccount(accountId);
        }

        public Task<decimal> GetLastBalanceFromAccount(Guid accountId)
        {
            return _transactionRepository.GetLastBalanceFromAccount(accountId);
        }
    }
}
