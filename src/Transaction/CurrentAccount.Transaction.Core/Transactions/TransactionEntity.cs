using CurrentAccount.Core.Shared;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Transaction.Core.Transactions
{
	public class TransactionEntity
	{
		public TransactionEntity(Guid transactionId,
								 Guid accountId,
								 RecordedDateValue transactionDate,
								 TransactionTypeEnum type,
								 DecimalNumberValue amount,
								 NameValue description,
								 DecimalNumberValue actualBalance,
							     CurrencyValue currency)
		{
			TransactionId = transactionId;
			AccountId = accountId;
			TransactionDate = transactionDate;
			Type = type;
			Amount = amount;
			Description = description;
			ActualBalance = actualBalance;
			Currency = currency;
		}

		public Guid TransactionId { get; private set; }
		public Guid AccountId { get; private set; }
		public RecordedDateValue TransactionDate { get; private set; }
		public TransactionTypeEnum Type { get; private set; }
		public DecimalNumberValue Amount { get; private set; }
		public NameValue Description { get; private set; }
		public DecimalNumberValue ActualBalance { get; private set; }
		public CurrencyValue Currency { get; private set; }

		public ResultModel<DecimalNumberValue> SetBalance(decimal balance)
		{
			var balanceResult = DecimalNumberValue.Create(balance);

			if (!balanceResult.IsSuccess) { return balanceResult; }

			ActualBalance = balanceResult.Value;

			return balanceResult;
		}
	}
}
