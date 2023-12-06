using CurrentAccount.Core.Shared.Transactions.Commands;
using EventBus.Messages.Events.Transactions;
using MassTransit;

namespace CurrentAccount.Transaction.Application.Transactions.Handlers
{
	public class CreateTransactionEventHandler : IConsumer<CurrentAccountTransactionEvent>
	{
		private readonly ICreateTransactionHandler _createTransactionHandler;
		public CreateTransactionEventHandler(ICreateTransactionHandler createTransactionHandler)
        {
            _createTransactionHandler = createTransactionHandler;
        }
        public Task Consume(ConsumeContext<CurrentAccountTransactionEvent> context)
		{
			var message = context.Message;
			var command = new CreateTransactionCommand(message.AccountId, message.TransactionType, message.Amount, message.Description, message.Currency);

			return _createTransactionHandler.HandleCreateTransaction(command);
		}
	}
}
