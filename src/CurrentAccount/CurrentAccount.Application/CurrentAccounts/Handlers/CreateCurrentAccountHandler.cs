using CurrentAccount.Application.CurrentAccounts.Commands;
using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Application.CurrentAccount.Handlers
{
    public class CreateCurrentAccountHandler : ICreateCurrentAccountHandler
    {
        // This a generic message for security reasons. This can be more specific depending on business rules.
        private readonly string _creatingAccountErrorMessage = "Error creating the account";

        private readonly ICustomerService _customerService;
        private readonly ICurrentAccountService _currentAccountService;

        public CreateCurrentAccountHandler(ICustomerService customerService, ICurrentAccountService currentAccountService)
        {
            _customerService = customerService;
            _currentAccountService = currentAccountService;
        }

        public async Task<ResultModel<Guid>> HandleExistentCustomer(CreateCurrentAccountCommand command)
        {
            // Get customer from CustomerId
            var existentCustomer = await _customerService.GetCustomerById(command.CustomerId);

            if (!existentCustomer.IsSuccess || existentCustomer.Value == null)
            {
                return ResultModel<Guid>.Failure(_creatingAccountErrorMessage);
            }

            // Because I do not have much of the business rules involved and I am receiving only a few parameters
            // I will create the new account based on the last account created for the same customer and with the same type (Businness, Individual)

            var customerLastCurrentAccount = await _currentAccountService.GetLastActiveAccountFromCustomer(existentCustomer.Value);

            if(!customerLastCurrentAccount.IsSuccess || customerLastCurrentAccount.Value == null)
            {
                return ResultModel<Guid>.Failure(_creatingAccountErrorMessage);
            }

            var currentAccountModified = customerLastCurrentAccount.Value;

            // Set the new account number
            var newAccountNumber = await _currentAccountService.CalculateNextAccountNumber();
            var accountNumberValue = AccountNumberValue.Create(newAccountNumber);

            if (!accountNumberValue.IsSuccess)
            {
                return ResultModel<Guid>.Failure(accountNumberValue.ErrorMessage);
            }

            currentAccountModified.SetAccountNumber(accountNumberValue.Value);

            // Set the new CustomerId
            var newAccountUuid = Guid.NewGuid();
            currentAccountModified.Customer.SetCustomerId(newAccountUuid);

            // Create account
            var customerId = await _currentAccountService.CreateCurrentAccount(currentAccountModified);

            return ResultModel<Guid>.Success(customerId);
        }
    }
}
