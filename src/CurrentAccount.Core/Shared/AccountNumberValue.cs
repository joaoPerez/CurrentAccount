using CurrentAccount.Core.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CurrentAccount.Core.Shared
{
    public record AccountNumberValue
    {
        private static readonly string _accountNumberErrorMessage = "The provided account number is not valid";

        private static readonly byte _maxAccountNumberSize = 10;
        private static readonly byte _minAccountNumberSize = 10;

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
                accountNumber.Length > _maxAccountNumberSize ||
                accountNumber.Length < _minAccountNumberSize)
            {
                return false;
            }

            return Regex.IsMatch(accountNumber, _regexAccountNumber, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}
