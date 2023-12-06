using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Application.CurrentAccount.Services
{
    public class CurrentAccountService : ICurrentAccountService
    {
        private readonly ICurrentAccountRepository _currentAccountRepository;

        public CurrentAccountService(ICurrentAccountRepository currentAccountRepository)
        {
            _currentAccountRepository = currentAccountRepository;
        }

        public Task<Guid> CreateCurrentAccount(CurrentAccountEntity currentAccount)
        {
            return _currentAccountRepository.CreateCurrentAccount(currentAccount);
        }

        // Simplifying the way to create an account number for this example code.
        public async Task<string> CalculateNextAccountNumber()
        {
            var lastActualAccountNumber = await GetLastCreatedAccountNumberOfAllClients();

            if (lastActualAccountNumber == null) { lastActualAccountNumber = "1"; }

            var integerAccountNumber = Convert.ToInt32(lastActualAccountNumber);
            var nextAccountNumber = ++integerAccountNumber;

            return nextAccountNumber.ToString().PadLeft(AccountNumberValue.AccountNumberSize, '0');
        }

        public Task<ResultModel<CurrentAccountEntity>> GetLastActiveAccountFromCustomer(CustomerEntity customer)
        {
            return _currentAccountRepository.GetLastActiveAccountFromCustomer(customer);
        }

        public Task<List<Guid>> GetCurrentAccountsFromCustomer(Guid customerId)
        {
            return _currentAccountRepository.GetCurrentAccountsFromCustomer(customerId);
        }

        private Task<string> GetLastCreatedAccountNumberOfAllClients()
        {
            return _currentAccountRepository.GetLastCreatedAccountNumber();
        }
    }
}
