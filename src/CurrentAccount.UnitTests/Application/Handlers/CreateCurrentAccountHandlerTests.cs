using CurrentAccount.Application.CurrentAccount.Dto;
using CurrentAccount.Application.CurrentAccount.Handlers;
using CurrentAccount.Application.CurrentAccount.Services;
using CurrentAccount.Core.Customer;
using Moq;

namespace CurrentAccount.UnitTests.Application.Handlers
{
	public class CreateCurrentAccountHandlerTests
	{
		private readonly ICurrentAccountAppFactory _accountAppFactory;
		private readonly Mock<ICustomerService> _customerServiceMock;
		private readonly Mock<ICurrentAccountService> _currentAccountServiceMock;

		private readonly ICreateCurrentAccountHandler _createCurrentAccountHandler;

		public CreateCurrentAccountHandlerTests()
        {
			_accountAppFactory = new CurrentAccountAppFactory();
			_customerServiceMock = new Mock<ICustomerService>();
			_currentAccountServiceMock = new Mock<ICurrentAccountService>();

			_createCurrentAccountHandler = new CreateCurrentAccountHandler(_customerServiceMock.Object, _currentAccountServiceMock.Object);
		}

		[Fact]
		public async Task HandleExistentCustomer_SuccessfullyCreatesAccount()
		{	
			var customerId = Guid.NewGuid();
			var command = new CreateCurrentAccountCommand(customerId, 0);

			var existingCustomer = new CustomerEntity(customerId);

			var accountDto = PopulateCurrentAccountDto();
			var accountEntity = _accountAppFactory.ToCurrentAccountEntity(Guid.NewGuid(), accountDto, existingCustomer);

			var lastActiveAccount = accountEntity.Value;

			_customerServiceMock.Setup(service => service.GetCustomerById(customerId))
				.ReturnsAsync(existingCustomer);

			_currentAccountServiceMock.Setup(service => service.GetLastActiveAccountFromCustomer(existingCustomer))
				.ReturnsAsync(lastActiveAccount);

			_currentAccountServiceMock.Setup(service => service.CalculateNextAccountNumber())
				.ReturnsAsync("0000000002"); // Assuming a string for simplicity

			_currentAccountServiceMock.Setup(service => service.CreateCurrentAccount(It.IsAny<CurrentAccountEntity>()))
				.ReturnsAsync(Guid.NewGuid()); // Assuming a new GUID for the created account

			var result = await _createCurrentAccountHandler.HandleExistentCustomer(command);

			Assert.True(result.IsSuccess);
			Assert.NotEqual(Guid.Empty, result.Value);
		}

		[Fact]
		public async Task HandleExistentCustomer_FailsWhenCustomerNotFound()
		{
			var customerId = Guid.NewGuid();
			var command = new CreateCurrentAccountCommand(customerId, 0);

			_customerServiceMock.Setup(service => service.GetCustomerById(customerId))
				.ReturnsAsync((CustomerEntity)null); // Customer not found

			var result = await _createCurrentAccountHandler.HandleExistentCustomer(command);

			Assert.False(result.IsSuccess);
			Assert.Equal("Error creating the account", result.ErrorMessage);
		}

		private static CurrentAccountDto PopulateCurrentAccountDto()
		{
			return new CurrentAccountDto
			{
				AccountNumber = "0123456789",
				AccountDigit = 0,
				AccountType = AccountTypeEnum.Individual,
				Balance = 1000.50m,
				Currency = "USD",
				Country = "Country",
				Street = "111 Street",
				City = "City",
				State = "State",
				ZipCode = "12345",
				CountryCode = "55",
				PhoneNumber = "55555555",
				Email = "example@email.com",
				IsActive = true,
				CreationDate = DateTime.Now,
				ClosingDate = null
			};
		}
	}
}
