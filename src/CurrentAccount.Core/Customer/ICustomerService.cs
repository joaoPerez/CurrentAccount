namespace CurrentAccount.Core.Customer
{
	public interface ICustomerService
	{
		Task<Guid> CreateCustomer(CustomerEntity customer);
		Task<CustomerEntity> GetCustomerById(Guid customerId);
	}
}
