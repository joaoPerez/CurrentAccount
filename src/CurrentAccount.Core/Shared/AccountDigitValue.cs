using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Core.Shared
{
    public record AccountDigitValue
    {
        private static readonly string _accountDigitErrorMessage = "The provided account digit is not valid";
        private static readonly byte _maxValueAccountDigit = 9;

        private AccountDigitValue(byte accountDigit)
        {
            AccountDigit = accountDigit;
        }

        public byte AccountDigit { get; init; }

        public static ResultModel<AccountDigitValue> Create(byte accountDigit)
        {
            if (!IsValidAccountNumber(accountDigit))
            {
                return ResultModel<AccountDigitValue>.Failure(_accountDigitErrorMessage);
            }

            return ResultModel<AccountDigitValue>.Success(new AccountDigitValue(accountDigit));
        }

        private static bool IsValidAccountNumber(byte accountDigit)
        {
            // Since this is a byte (non negative values) type the system don't need to test the min value.
            if (accountDigit > _maxValueAccountDigit)
            {
                return false;
            }

            return true;
        }
    }
}
