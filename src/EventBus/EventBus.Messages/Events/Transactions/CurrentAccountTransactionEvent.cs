namespace EventBus.Messages.Events.Transactions
{
	public class CurrentAccountTransactionEvent : IntegrationBaseEvent
	{
        public CurrentAccountTransactionEvent(Guid accountId, string transactionType,
			decimal amount, string description, string currency)
        {
            AccountId = accountId;
			TransactionType = transactionType;
			Amount = amount;
			Description = description;
			Currency = currency;
        }

        public Guid AccountId { get; private set; }
		public string TransactionType { get; private set; }
		public decimal Amount { get; private set; }
		public string Description { get; private set; }
		public string Currency { get; private set; }
	}
}
