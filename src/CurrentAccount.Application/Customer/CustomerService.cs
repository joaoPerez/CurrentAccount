using CurrentAccount.Core.Customer;

namespace CurrentAccount.Application.Customer
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public Task<Guid> CreateCustomer(CustomerEntity customer)
		{
			return _customerRepository.CreateCustomer(customer);
		}

		public Task<CustomerEntity> GetCustomerById(Guid customerId)
		{
			return _customerRepository.GetCustomerById(customerId);
		}
	}
}
