using CurrentAccount.Application.Customer;
using CurrentAccount.Core.Customer;
using Moq;

namespace CurrentAccount.UnitTests.Core.Customer
{
	public class CustomerServiceTests
	{
		private readonly Mock<ICustomerRepository> _customerRepositoryMock;
		private readonly ICustomerService _customerService;

		public CustomerServiceTests()
		{
			_customerRepositoryMock = new Mock<ICustomerRepository>();
			_customerService = new CustomerService(_customerRepositoryMock.Object);
		}

		[Fact]
		public async Task CreateCustomer_ValidData_ShouldReturnsCustomerId()
		{
			var customerEntity = new CustomerEntity(Guid.NewGuid());

			_customerRepositoryMock.Setup(repo => repo.CreateCustomer(It.IsAny<CustomerEntity>()))
				.ReturnsAsync(Guid.NewGuid());

			var result = await _customerService.CreateCustomer(customerEntity);

			Assert.IsType<Guid>(result);
		}

		[Fact]
		public async Task GetCustomerById_ValidData_ShouldReturnCustomerEntity()
		{
			var customerId = Guid.NewGuid();
			var mockCustomerEntity = new CustomerEntity(customerId);

			_customerRepositoryMock.Setup(repo => repo.GetCustomerById(customerId))
				.ReturnsAsync(mockCustomerEntity);

			var result = await _customerService.GetCustomerById(customerId);

			Assert.NotNull(result);
			Assert.IsType<CustomerEntity>(result);
			Assert.Equal(customerId, result.CustomerId);
		}

		[Fact]
		public async Task CreateIndividualCustomer_ValidData_ShouldReturnCustomerId()
		{
			var firstNameValue = NameValue.Create("Right");
			var lastNameValue = NameValue.Create("Name");

			var individualCustomerEntity = new IndividualCustomerEntity(Guid.NewGuid(), firstNameValue.Value, lastNameValue.Value);

			_customerRepositoryMock.Setup(repo => repo.CreateCustomer(It.IsAny<CustomerEntity>()))
				.ReturnsAsync(Guid.NewGuid());

			var result = await _customerService.CreateCustomer(individualCustomerEntity);

			Assert.IsType<Guid>(result);
		}

		[Fact]
		public async Task CreateIndividualCustomer_ValidData_ShouldPassCorrectDataToRepository()
		{
			var customerId = Guid.NewGuid();

			var firstNameValue = NameValue.Create("Right");
			var lastNameValue = NameValue.Create("Name");

			var firstName = firstNameValue.Value;
			var lastName = lastNameValue.Value;

			var individualCustomerEntity = new IndividualCustomerEntity(customerId, firstNameValue.Value, lastNameValue.Value);

			await _customerService.CreateCustomer(individualCustomerEntity);

			_customerRepositoryMock.Verify(repo => repo.CreateCustomer(It.Is<IndividualCustomerEntity>(
				entity => entity.CustomerId == customerId &&
						  entity.FirstName == firstName &&
						  entity.LastName == lastName
			)), Times.Once);
		}

		[Fact]
		public async Task CreateBusinessCustomer_ValidData_ShouldReturnCustomerId()
		{
			var businessNameValue = NameValue.Create("Business name");
			var businessTypeValue = BusinessTypeValue.Create("SmallBusiness");

			var businessCustomerEntity = new BusinessCustomerEntity(Guid.NewGuid(), businessNameValue.Value, businessTypeValue.Value);

			_customerRepositoryMock.Setup(repo => repo.CreateCustomer(It.IsAny<CustomerEntity>()))
				.ReturnsAsync(Guid.NewGuid());

			var result = await _customerService.CreateCustomer(businessCustomerEntity);

			Assert.IsType<Guid>(result);
		}

		[Fact]
		public async Task CreateBusinessCustomer_ValidData_ShouldPassCorrectDataToRepository()
		{
			var customerId = Guid.NewGuid();

			var businessNameValue = NameValue.Create("Business name");
			var businessTypeValue = BusinessTypeValue.Create("SmallBusiness");

			var businessName = businessNameValue.Value;
			var businessType = businessTypeValue.Value;

			var individualCustomerEntity = new BusinessCustomerEntity(customerId, businessName, businessType);

			await _customerService.CreateCustomer(individualCustomerEntity);

			_customerRepositoryMock.Verify(repo => repo.CreateCustomer(It.Is<BusinessCustomerEntity>(
				entity => entity.CustomerId == customerId &&
						  entity.BusinessName == businessName &&
						  entity.BusinessType == businessType
			)), Times.Once);
		}
	}
}
