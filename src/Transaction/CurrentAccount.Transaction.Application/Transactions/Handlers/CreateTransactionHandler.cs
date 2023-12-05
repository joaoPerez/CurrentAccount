using CurrentAccount.Core.Shared;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Application.Transactions.Commands;
using CurrentAccount.Transaction.Core.Transactions;

namespace CurrentAccount.Transaction.Application.Transactions.Handlers
{
	public class CreateTransactionHandler : ICreateTransactionHandler
	{
		private static readonly string _transactionErrorMessage = "The transaction could not be created";

		private readonly ITransactionService _transactionService;

        public CreateTransactionHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<ResultModel<Guid>> HandleCreateTransaction(CreateTransactionCommand command)
		{
			var lastBalance = await _transactionService.GetLastBalanceFromAccount(command.accountId);

			var transactionEntity = FromCommandToEntity(command, lastBalance);

			if(!transactionEntity.IsSuccess) 
			{
				return ResultModel<Guid>.Failure(transactionEntity.ErrorMessage);
			}

			return await _transactionService.CreateTransaction(transactionEntity.Value);
		}

		private static ResultModel<TransactionEntity> FromCommandToEntity(CreateTransactionCommand command, decimal actualBalanceResult)
		{
			var transactionId = Guid.NewGuid();
			var accountUuid = command.accountId;

			var transactionDate = RecordedDateValue.Create(DateTime.Now);

			if (!transactionDate.IsSuccess)
			{
				return ResultModel<TransactionEntity>.Failure(_transactionErrorMessage);
			}

			TransactionTypeEnum? transactionType = Enum.GetName(TransactionTypeEnum.Credit).Equals(command.transactionType) ? TransactionTypeEnum.Credit
				: Enum.GetName(TransactionTypeEnum.Debit).Equals(command.transactionType) ? TransactionTypeEnum.Debit : null;

			if (transactionType == null) { return ResultModel<TransactionEntity>.Failure(_transactionErrorMessage); }

			var amount = DecimalNumberValue.Create(command.amount);

			if (!amount.IsSuccess)
			{
				return ResultModel<TransactionEntity>.Failure(_transactionErrorMessage);
			}

			ResultModel<NameValue>? description;

			if (!string.IsNullOrWhiteSpace(command.description))
			{
				description = NameValue.Create(command.description);

				if (!description.IsSuccess)
				{
					return ResultModel<TransactionEntity>.Failure(_transactionErrorMessage);
				}
			}
			else
			{
				description = null;
			}

			var currency = CurrencyValue.Create(command.currency);

			if (!currency.IsSuccess)
			{
				return ResultModel<TransactionEntity>.Failure(_transactionErrorMessage);
			}

			var actualBalance = DecimalNumberValue.Create(actualBalanceResult);

			if (!actualBalance.IsSuccess)
			{
				return ResultModel<TransactionEntity>.Failure(_transactionErrorMessage);
			}

			var transaction = new TransactionEntity(transactionId,
														  accountUuid,
														  transactionDate.Value,
														  transactionType.Value,
														  amount.Value,
														  description?.Value,
														  actualBalance.Value,
														  currency.Value
				);

			return ResultModel<TransactionEntity>.Success(transaction);
		}
	}
}
