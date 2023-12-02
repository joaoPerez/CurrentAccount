using Moq;
using FluentAssertions;
using CurrentAccount.Core.Customer;
using CurrentAccount.Application.CurrentAccount.Dto;
using CurrentAccount.Application.CurrentAccount.Services;

namespace CurrentAccount.UnitTests.Core.CurrentAccount
{
	public class CurrentAccountServiceTests
	{
		private readonly ICurrentAccountService _currentAccountService;
		private readonly ICurrentAccountAppService _currentAccountAppService;

		private readonly Mock<ICurrentAccountRepository> _currentAccountRepositoryMock;

		private readonly CurrentAccountDto _defaultCurrentAccountDto;
		private readonly CustomerEntity _defaultCustomerEntity;
		private readonly CurrentAccountEntity _defaultCurrentAccountEntity;

		public CurrentAccountServiceTests()
        {
			_currentAccountRepositoryMock = new Mock<ICurrentAccountRepository>();
			_currentAccountService = new CurrentAccountService(_currentAccountRepositoryMock.Object);

			Guid accountId = Guid.NewGuid();
			_defaultCurrentAccountDto = PopulateCurrentAccountDto();
			_defaultCustomerEntity = new CustomerEntity(Guid.NewGuid());

			_currentAccountAppService = new CurrentAccountAppService(accountId, _defaultCurrentAccountDto, _defaultCustomerEntity);

			_defaultCurrentAccountEntity = _currentAccountAppService.ToCurrentAccountEntity().Value;
		}

        [Fact]
		public async Task CreateCurrentAccount_RightData_ShouldReturnAccountId()
		{
			var currentAccount = _currentAccountAppService.ToCurrentAccountEntity();

			_currentAccountRepositoryMock
				.Setup(repo => repo.CreateCurrentAccount(It.IsAny<CurrentAccountEntity>()))
				.ReturnsAsync(Guid.NewGuid());

			var result = await _currentAccountService.CreateCurrentAccount(currentAccount.Value);

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

			_currentAccountRepositoryMock
				.Setup(repo => repo.GetLastActiveAccountFromCustomer(It.IsAny<CustomerEntity>()))
				.ReturnsAsync(currentAccount);

			var result = await _currentAccountService.GetLastActiveAccountFromCustomer(customer);

			result.Should().NotBeNull();
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
