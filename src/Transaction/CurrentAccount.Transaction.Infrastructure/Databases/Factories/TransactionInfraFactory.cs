using CurrentAccount.Core.Shared;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Core.Transactions;
using CurrentAccount.Transaction.Infrastructure.Databases.Models;

namespace CurrentAccount.Transaction.Infrastructure.Databases.Factories
{
	public class TransactionInfraFactory : ITransactionInfraFactory
	{
		private readonly string _populateEntityError = "Error retrieving the transaction";

		public ResultModel<TransactionEntity> ToCurrentAccountEntity(TransactionDataModel accountDataModel)
		{

			if (string.IsNullOrWhiteSpace(accountDataModel.TransactionType)) 
			{
				return ResultModel<TransactionEntity>.Failure(_populateEntityError);
			}

			var transactionTypeEnum = Enum.GetName(TransactionTypeEnum.Credit).Equals(accountDataModel.TransactionType) ? TransactionTypeEnum.Credit : TransactionTypeEnum.Debit;

			var transactionDateValue = RecordedDateValue.Create(accountDataModel.TransactionDate);

			if(!transactionDateValue.IsSuccess)
			{
				return ResultModel<TransactionEntity>.Failure(_populateEntityError);
			}

			var amountValue = DecimalNumberValue.Create(accountDataModel.Amount);

			if(!amountValue.IsSuccess)
			{
				return ResultModel<TransactionEntity>.Failure(_populateEntityError);
			}

			NameValue descriptionValue = null;

			if(!string.IsNullOrWhiteSpace(accountDataModel.Description))
			{
				var description = NameValue.Create(accountDataModel.Description);

				if(!description.IsSuccess) 
				{
					return ResultModel<TransactionEntity>.Failure(_populateEntityError);
				}

				descriptionValue = description.Value;
			}

			var actualBalanceValue = DecimalNumberValue.Create(accountDataModel.ActualBalance);

			if (!actualBalanceValue.IsSuccess)
			{
				return ResultModel<TransactionEntity>.Failure(_populateEntityError);
			}

			var currencyValue = CurrencyValue.Create(accountDataModel.Currency);

			if(!currencyValue.IsSuccess) 
			{
				return ResultModel<TransactionEntity>.Failure(_populateEntityError);
			}

			var entity = new TransactionEntity(accountDataModel.Id,
				accountDataModel.CurrentAccountId, transactionDateValue.Value, transactionTypeEnum,
				amountValue.Value, descriptionValue, actualBalanceValue.Value, currencyValue.Value);

			return ResultModel<TransactionEntity>.Success(entity);
		}
	}
}
