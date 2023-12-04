using CurrentAccount.Core.Shared.Result;
using System.Text.RegularExpressions;

namespace CurrentAccount.Core.Shared
{
    public record AccountNumberValue
    {
        private static readonly string _accountNumberErrorMessage = "The provided account number is not valid";

        public static readonly byte AccountNumberSize = 10;

        private static readonly string _regexAccountNumber = @"^\d+(\.\d+)?$";

        private AccountNumberValue(string accountNumber)
        {
            AccountNumber = accountNumber;
        }
        public string AccountNumber { get; init; }

        public static ResultModel<AccountNumberValue> Create(string accountNumber)
        {
            if (!IsValidAccountNumber(accountNumber))
            {
                return ResultModel<AccountNumberValue>.Failure(_accountNumberErrorMessage);
            }

            return ResultModel<AccountNumberValue>.Success(new AccountNumberValue(accountNumber));
        }

        private static bool IsValidAccountNumber(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber) ||
                accountNumber.Length != AccountNumberSize)
            {
                return false;
            }

            return Regex.IsMatch(accountNumber, _regexAccountNumber, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}
