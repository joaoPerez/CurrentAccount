using Moq;
using FluentAssertions;
using CurrentAccount.Core.Customer;
using CurrentAccount.Application.CurrentAccount.Services;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Infrastructure.Database.Models.CurrentAccount;
using CurrentAccount.Infrastructure.Database.Models;

namespace CurrentAccount.UnitTests.Core.CurrentAccount
{
	public class CurrentAccountServiceTests
	{
		private readonly ICurrentAccountService _currentAccountService;
		private readonly ICurrentAccountInfraFactory _currentAccountInfraFactory;

		private readonly Mock<ICurrentAccountRepository> _currentAccountRepositoryMock;

		private readonly CurrentAccountDataModel _defaultCurrentAccountDataModel;
		private readonly CustomerEntity _defaultCustomerEntity;
		private readonly CurrentAccountEntity _defaultCurrentAccountEntity;

		public CurrentAccountServiceTests()
        {
			_currentAccountRepositoryMock = new Mock<ICurrentAccountRepository>();
			_currentAccountService = new CurrentAccountService(_currentAccountRepositoryMock.Object);

			Guid accountId = Guid.NewGuid();
			_defaultCurrentAccountDataModel = PopulateCurrentAccountDataModel();
			_defaultCustomerEntity = new CustomerEntity(Guid.NewGuid());

			_currentAccountInfraFactory = new CurrentAccountInfraFactory();

			_defaultCurrentAccountEntity = _currentAccountInfraFactory.ToCurrentAccountEntity(_defaultCurrentAccountDataModel).Value;
		}

        [Fact]
		public async Task CreateCurrentAccount_RightData_ShouldReturnAccountId()
		{
			_currentAccountRepositoryMock
				.Setup(repo => repo.CreateCurrentAccount(It.IsAny<CurrentAccountEntity>()))
				.ReturnsAsync(Guid.NewGuid());

			var result = await _currentAccountService.CreateCurrentAccount(_defaultCurrentAccountEntity);

			result.Should().NotBeEmpty();
		}

		[Fact]
		public async Task CalculateNextAccountNumber_RightData_ShouldReturnNextAccountNumber()
		{
			var actualValue = "0000000100";
			var expectedValue = "0000000101";

			_currentAccountRepositoryMock
				.Setup(repo => repo.GetLastCreatedAccountNumber())
				.ReturnsAsync(actualValue);

			var result = await _currentAccountService.CalculateNextAccountNumber();

			result.Should().Be(expectedValue);
		}

		[Fact]
		public async Task GetLastActiveAccountFromCustomer_RightData_ShouldReturnAccount()
		{
			var customer = _defaultCustomerEntity;
			var currentAccount = _defaultCurrentAccountEntity;
			var resultCustomer = ResultModel<CurrentAccountEntity>.Success(currentAccount);

			_currentAccountRepositoryMock
				.Setup(repo => repo.GetLastActiveAccountFromCustomer(It.IsAny<CustomerEntity>()))
				.ReturnsAsync(resultCustomer);

			var result = await _currentAccountService.GetLastActiveAccountFromCustomer(customer);

			result.Should().NotBeNull();
		}

		private static CurrentAccountDataModel PopulateCurrentAccountDataModel()
		{
			var customerId = Guid.NewGuid();
			var contactInfoId = Guid.NewGuid();
			var accountHolderAddressId = Guid.NewGuid();

			return new CurrentAccountDataModel
			{
				Id = Guid.NewGuid(),
				AccountDigit = 0,
				AccountHolderAddressId = accountHolderAddressId,
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
				ContactInfoId = contactInfoId,
				ContactInfo = new ContactInformationDataModel
				{
					Id = Guid.NewGuid(),
					Email = "test@email.com",
					PhoneNumber = "555555555",
				},
				CreationDate = DateTime.Now,
				Currency = "USD",
				CustomerId = customerId,
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
