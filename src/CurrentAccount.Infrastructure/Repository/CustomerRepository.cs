using CurrentAccount.Core.Customer;

namespace CurrentAccount.Infrastructure.Repository
{
	public class CustomerRepository : ICustomerRepository
	{
		public Task<Guid> CreateCustomer(CustomerEntity customer)
		{
			throw new NotImplementedException();
		}

		public Task<CustomerEntity> GetCustomerById(Guid customerId)
		{
			throw new NotImplementedException();
		}
	}
}
