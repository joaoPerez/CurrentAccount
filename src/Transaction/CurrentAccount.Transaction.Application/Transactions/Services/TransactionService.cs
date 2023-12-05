using CurrentAccount.Core.Shared.Result;
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

        public Task<Guid> CreateTransaction(TransactionEntity transaction)
        {
            return _transactionRepository.CreateTransaction(transaction);
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
