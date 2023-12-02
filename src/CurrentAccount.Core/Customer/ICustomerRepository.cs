namespace CurrentAccount.Core.Customer
{
	public interface ICustomerRepository
	{
		Task<Guid> CreateCustomer(CustomerEntity customer);
		Task<CustomerEntity> GetCustomerById(Guid customerId);
	}
}
