using CurrentAccount.Core.Shared.Transactions.Commands;
using EventBus.Messages.Events.Transactions;
using MassTransit;

namespace CurrentAccount.Application.Transactions
{
	public class CreateTransactionHandler : ICreateTransactionHandler
	{
		private readonly IBus _publishEndpoint;
        public CreateTransactionHandler(IBus publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task HandleTransactionEvent(CreateTransactionCommand command)
		{
			var transactionEvent = new CurrentAccountTransactionEvent(
				command.accountId,
				command.transactionType,
				command.amount,
				command.description,
				command.currency);

			return _publishEndpoint.Publish(transactionEvent);
		}
	}
}
