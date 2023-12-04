using CurrentAccount.Core.Shared;

namespace CurrentAccount.Transaction.Core.Transactions
{
	public class TransactionEntity
	{
		public Guid TransactionId { get; private set; }
		public Guid AccountId { get; private set; }
		public RecordedDateValue TransactionDateTime { get; private set; }
		public TransactionTypeEnum Type { get; private set; }
		public DecimalNumberValue Amount { get; private set; }
		public NameValue Description { get; private set; }
		public DecimalNumberValue ActualBalance { get; private set; }
		public CurrencyValue Currency { get; private set; }
	}
}
