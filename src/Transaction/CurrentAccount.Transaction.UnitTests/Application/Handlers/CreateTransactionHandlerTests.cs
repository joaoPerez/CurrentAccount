using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Application.Transactions.Commands;
using CurrentAccount.Transaction.Application.Transactions.Handlers;
using CurrentAccount.Transaction.Core.Transactions;

namespace CurrentAccount.Transaction.UnitTests.Application.Handlers
{
	public class CreateTransactionHandlerTests
	{
		private readonly Mock<ITransactionService> _transactionServiceMock;
		private readonly ICreateTransactionHandler _createTransactionHandler;
		private readonly CreateTransactionCommand _createTransactionCommand;

        public CreateTransactionHandlerTests()
        {

			_transactionServiceMock = new Mock<ITransactionService>();
			_createTransactionHandler = new CreateTransactionHandler(_transactionServiceMock.Object);

			Guid accountId = Guid.NewGuid();
			string transactionType = Enum.GetName(TransactionTypeEnum.Credit);
			decimal amount = 0;
			string description = string.Empty;
			string currency = "USD";

			_createTransactionCommand = new CreateTransactionCommand(accountId, transactionType, amount, description, currency);
		}

        [Fact]
		public async Task HandleCreateTransaction_ValidData_ShouldReturnSuccess()
		{
			// Return a valid actual balance
			_transactionServiceMock.Setup(service => service.GetLastBalanceFromAccount(_createTransactionCommand.accountId))
								  .ReturnsAsync(100.0m);

			// Return a valid transaction ID
			_transactionServiceMock.Setup(service => service.CreateTransaction(It.IsAny<TransactionEntity>()))
								  .ReturnsAsync(ResultModel<Guid>.Success(Guid.NewGuid()));

			var result = await _createTransactionHandler.HandleCreateTransaction(_createTransactionCommand);

			Assert.True(result.IsSuccess);
			Assert.NotEqual(Guid.Empty, result.Value);
		}

		[Fact]
		public async Task HandleCreateTransaction_CreateError_ShouldReturnFailure()
		{
			var mockTransactionService = new Mock<ITransactionService>();
			var handler = new CreateTransactionHandler(mockTransactionService.Object);

			_transactionServiceMock.Setup(service => service.GetLastBalanceFromAccount(_createTransactionCommand.accountId))
								  .ReturnsAsync(100.0m); // Set the expected actual balance

			_transactionServiceMock.Setup(service => service.CreateTransaction(It.IsAny<TransactionEntity>()))
								  .ReturnsAsync(ResultModel<Guid>.Failure("Error")); // Simulate failure

			var result = await _createTransactionHandler.HandleCreateTransaction(_createTransactionCommand);

			Assert.False(result.IsSuccess);
			Assert.Equal("Error", result.ErrorMessage);
		}
	}
}
