using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Core.Transactions;
using CurrentAccount.Transaction.Infrastructure.Databases.Contexts;
using CurrentAccount.Transaction.Infrastructure.Databases.Factories;
using CurrentAccount.Transaction.Infrastructure.Databases.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrentAccount.Transaction.Infrastructure.Databases.Repository
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly TransactionDbContext _dbContext;
		private readonly ITransactionInfraFactory _transactionInfraFactory;

		public TransactionRepository(ITransactionInfraFactory transactionInfraFactory, TransactionDbContext transactionContext)
        {
            _dbContext = transactionContext;
			_transactionInfraFactory = transactionInfraFactory;
        }

        public Task<Guid> CreateTransaction(TransactionEntity transaction)
		{
			var transactionId = Guid.NewGuid();

			var dataModel = new TransactionDataModel
			{
				Id = transactionId,
				CurrentAccountId = transaction.AccountId,
				ActualBalance = transaction.ActualBalance.Value,
				Amount = transaction.Amount.Value,
				Currency = transaction.Currency.Currency,
				Description = transaction.Description.Name,
				TransactionType = Enum.GetName(transaction.Type),
				TransactionDate = transaction.TransactionDate.RecordedDate.ToUniversalTime()
			};

			_dbContext.Add(dataModel);
			_dbContext.SaveChanges();

			return Task.FromResult(transactionId);
		}

		public async Task<ResultModel<List<TransactionEntity>>> GetAllTransactionsFromAccount(Guid accountId)
		{
			// Just getting every data without data filtering and pagination because this is an example system.
			var transactions = await _dbContext.Transactions.Where(x => x.CurrentAccountId == accountId).ToListAsync();

			var transactionEntities = new List<TransactionEntity>(transactions.Count);

			foreach(var transaction in transactions)
			{
				var transactionEntity = _transactionInfraFactory.ToCurrentAccountEntity(transaction);

				if(!transactionEntity.IsSuccess) 
				{ 
					return ResultModel<List<TransactionEntity>>.Failure(transactionEntity.ErrorMessage); 
				}

				transactionEntities.Add(transactionEntity.Value);
			}

			return ResultModel<List<TransactionEntity>>.Success(transactionEntities);
		}

		public async Task<decimal> GetLastBalanceFromAccount(Guid accountId)
		{
			var lastBalance = await _dbContext.Transactions
				.OrderByDescending(x => x.TransactionDate)
				.Select(x => new { x.ActualBalance })
				.FirstOrDefaultAsync();

			if (lastBalance == null) return 0;

			return lastBalance.ActualBalance;
		}
	}
}
