using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Infrastructure.Database.Context;
using CurrentAccount.Infrastructure.Database.Models.Customer;
using Microsoft.EntityFrameworkCore;

namespace CurrentAccount.Infrastructure.Database.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
		private readonly CurrentAccountContext _dbContext;

        public CustomerRepository(CurrentAccountContext currentAccountContext)
        {
			_dbContext = currentAccountContext;
		}

        public Task<Guid> CreateCustomer(CustomerEntity customer)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultModel<CustomerEntity>> GetCustomerById(Guid customerId)
        {
			var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId.Equals(customerId));

            if(customer == null) { return ResultModel<CustomerEntity>.Success(null); }

            var individualCustomer = customer as IndividualCustomerDataModel;

			var firstName = individualCustomer.FirstName;
            var lastName = individualCustomer.LastName;

            var firstNameValue = NameValue.Create(firstName);

			if (!firstNameValue.IsSuccess)
			{
				return ResultModel<CustomerEntity>.Failure(firstNameValue.ErrorMessage);
			}

			var lastNameValue = NameValue.Create(lastName);

			if (!lastNameValue.IsSuccess)
			{
				return ResultModel<CustomerEntity>.Failure(lastNameValue.ErrorMessage);
			}

			var customerEntity = new IndividualCustomerEntity(customer.CustomerId, firstNameValue.Value, lastNameValue.Value);

            return ResultModel<CustomerEntity>.Success(customerEntity);
        }
    }
}
