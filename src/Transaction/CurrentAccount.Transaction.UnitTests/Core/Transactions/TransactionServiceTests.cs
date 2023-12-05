using CurrentAccount.Core.Shared;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Application.Transactions.Services;
using CurrentAccount.Transaction.Core.Transactions;
using Moq;

namespace CurrentAccount.Transaction.UnitTests.Core.Transactions
{
	public class TransactionServiceTests
	{
		private readonly ITransactionService _transactionService;
		private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
		private readonly TransactionEntity _transactionEntity;

		public TransactionServiceTests()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
			_transactionService = new TransactionService(_transactionRepositoryMock.Object);

			var transactionDate = RecordedDateValue.Create(DateTime.Now.AddDays(-1));
			var amount = DecimalNumberValue.Create(100.1m);
			var description = NameValue.Create("description");
			var actualBalance = DecimalNumberValue.Create(100);
			var currencyValue = CurrencyValue.Create("EUR"); 

			_transactionEntity = new TransactionEntity(
				Guid.NewGuid(),
				Guid.NewGuid(),
				transactionDate.Value,
				TransactionTypeEnum.Credit,
				amount.Value,
				description.Value,
				actualBalance.Value,
				currencyValue.Value
				);
		}

        [Fact]
		public async Task CreateTransaction_ValidData_ShouldReturnNonEmptyGuid()
		{
			var expectedTransactionUuid = Guid.NewGuid();

			_transactionRepositoryMock.Setup(repo => repo.CreateTransaction(It.IsAny<TransactionEntity>()))
						  .ReturnsAsync(expectedTransactionUuid);

			var result = await _transactionService.CreateTransaction(_transactionEntity);

			Assert.Equal(expectedTransactionUuid, result.Value);
		}

		[Fact]
		public async Task GetAllTransactionsFromAccount_ValidData_ShouldReturnExpectedResult()
		{
			var accountId = Guid.NewGuid();

			var expectedData = ResultModel<List<TransactionEntity>>.Success(
				new List<TransactionEntity>
				{
					_transactionEntity
				});

			_transactionRepositoryMock.Setup(repo => repo.GetAllTransactionsFromAccount(accountId))
						  .ReturnsAsync(expectedData);

			var result = await _transactionService.GetTransactionsFromAccount(accountId);

			Assert.NotNull(result);
			Assert.Equal(expectedData.Value, result.Value);
		}

		[Fact]
		public async Task GetLastBalanceFromAccount_ValidData_ShouldReturnExpectedValue()
		{
			var accountId = Guid.NewGuid();
			var expectedBalance = 123.45m;

			_transactionRepositoryMock.Setup(repo => repo.GetLastBalanceFromAccount(accountId))
						  .ReturnsAsync(expectedBalance);

			var result = await _transactionService.GetLastBalanceFromAccount(accountId);

			Assert.Equal(expectedBalance, result);
		}
	}
}
