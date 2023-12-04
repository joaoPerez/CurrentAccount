using CurrentAccount.Application.CurrentAccount.Dto;
using CurrentAccount.Application.CurrentAccount.Handlers;
using CurrentAccount.Application.CurrentAccount.Services;
using CurrentAccount.Core.Customer;
using CurrentAccount.Infrastructure.Database.Models.CurrentAccount;
using CurrentAccount.Infrastructure.Database.Models;
using Moq;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.UnitTests.Application.Handlers
{
	public class CreateCurrentAccountHandlerTests
	{
		private readonly ICurrentAccountInfraFactory _currentAccountInfraFactory;
		private readonly Mock<ICustomerService> _customerServiceMock;
		private readonly Mock<ICurrentAccountService> _currentAccountServiceMock;

		private readonly ICreateCurrentAccountHandler _createCurrentAccountHandler;

		public CreateCurrentAccountHandlerTests()
        {
			_currentAccountInfraFactory = new CurrentAccountInfraFactory();
			_customerServiceMock = new Mock<ICustomerService>();
			_currentAccountServiceMock = new Mock<ICurrentAccountService>();

			_createCurrentAccountHandler = new CreateCurrentAccountHandler(_customerServiceMock.Object, _currentAccountServiceMock.Object);
		}

		[Fact]
		public async Task HandleExistentCustomer_SuccessfullyCreatesAccount()
		{	
			var customerId = Guid.NewGuid();
			var command = new CreateCurrentAccountCommand(customerId, 0);

			var accountDataModel = PopulateCurrentAccountDataModel();
			var accountEntityResult = _currentAccountInfraFactory.ToCurrentAccountEntity(accountDataModel);

			var existingCustomerResult = ResultModel<CustomerEntity>.Success(new CustomerEntity(customerId));

			_customerServiceMock.Setup(service => service.GetCustomerById(customerId))
				.ReturnsAsync(existingCustomerResult);

			_currentAccountServiceMock.Setup(service => service.GetLastActiveAccountFromCustomer(existingCustomerResult.Value))
				.ReturnsAsync(accountEntityResult);

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

			var customerEntityNull = ResultModel<CustomerEntity>.Success(null);

			_customerServiceMock.Setup(service => service.GetCustomerById(customerId))
				.ReturnsAsync(customerEntityNull); // Customer not found

			var result = await _createCurrentAccountHandler.HandleExistentCustomer(command);

			Assert.False(result.IsSuccess);
			Assert.Equal("Error creating the account", result.ErrorMessage);
		}

		private static CurrentAccountDataModel PopulateCurrentAccountDataModel()
		{
			return new CurrentAccountDataModel
			{
				Id = Guid.NewGuid(),
				AccountDigit = 0,
				AccountHolderAddress = new AccountHolderAddressDataModel
				{
					Id = Guid.NewGuid(),
					City = "test city",
					Country = "test country",
					State = "test state",
					Street = "street test",
					ZipCode = "111111"
				},
				AccountNumber = "0123456789",
				AccountType = Enum.GetName(AccountTypeEnum.Business),
				IsActive = true,
				Balance = 1000.50m,
				ContactInfo = new ContactInformationDataModel
				{
					Id = Guid.NewGuid(),
					Email = "test@email.com",
					PhoneNumber = "555555555",
					CountryCode = "55",
				},
				CreationDate = DateTime.Now,
				Currency = "USD",
				Customer = new Infrastructure.Database.Models.Customer.IndividualCustomerDataModel
				{
					CustomerId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
					FirstName = "first name",
					LastName = "last name",
				}
			};
		}
	}
}
