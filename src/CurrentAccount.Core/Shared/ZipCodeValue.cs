using CurrentAccount.Core.Shared.Result;
using System.Text.RegularExpressions;

namespace CurrentAccount.Core.Shared
{
    public record ZipCodeValue
    {
        private static readonly string _zipCodeErrorMessage = "The provided zip code is not valid";

        private static readonly byte _maxZipCodeSize = 20;
        private static readonly byte _minZipCodeSize = 4;

        private static readonly string _regexZipCode = @"^\d+(\.\d+)?$";

        private ZipCodeValue(string zipCode)
        {
            ZipCode = zipCode;
        }

        public string ZipCode { get; init; }

        public static ResultModel<ZipCodeValue> Create(string accountNumber)
        {
            if (!IsValidZipCode(accountNumber))
            {
                return ResultModel<ZipCodeValue>.Failure(_zipCodeErrorMessage);
            }

            return ResultModel<ZipCodeValue>.Success(new ZipCodeValue(accountNumber));
        }

        private static bool IsValidZipCode(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber) ||
                accountNumber.Length > _maxZipCodeSize ||
                accountNumber.Length < _minZipCodeSize)
            {
                return false;
            }

            return Regex.IsMatch(accountNumber, _regexZipCode, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}
