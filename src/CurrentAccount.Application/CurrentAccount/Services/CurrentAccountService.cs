using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared;

namespace CurrentAccount.Application.CurrentAccount.Services
{
    public class CurrentAccountService : ICurrentAccountService
    {
        private readonly ICurrentAccountRepository _currentAccountRepository;

        public CurrentAccountService(ICurrentAccountRepository currentAccountRepository)
        {
            _currentAccountRepository = currentAccountRepository;
        }

        public async Task<Guid> CreateCurrentAccount(CurrentAccountEntity currentAccount)
        {
            return await _currentAccountRepository.CreateCurrentAccount(currentAccount);
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

        public async Task<CurrentAccountEntity> GetLastActiveAccountFromCustomer(CustomerEntity customer)
        {
            return await _currentAccountRepository.GetLastActiveAccountFromCustomer(customer);
        }

        private async Task<string> GetLastCreatedAccountNumberOfAllClients()
        {
            return await _currentAccountRepository.GetLastCreatedAccountNumber();
        }
    }
}
