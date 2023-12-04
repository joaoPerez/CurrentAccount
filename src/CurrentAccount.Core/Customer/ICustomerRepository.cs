using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Core.Customer
{
	public interface ICustomerRepository
	{
		Task<Guid> CreateCustomer(CustomerEntity customer);
		Task<ResultModel<CustomerEntity>> GetCustomerById(Guid customerId);
	}
}
