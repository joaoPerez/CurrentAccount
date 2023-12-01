using CurrentAccount.Core.Shared.Result;
using System.Text.RegularExpressions;

namespace CurrentAccount.Core.Shared
{
	public record PhoneNumberValue
	{
		private static readonly string _notValidPhoneNumberErrorMessage = "The provided phone number is not valid";
		private static readonly string _regexNumberValidation = @"^\d+(\.\d+)?$";
		private static readonly byte _maxLengthPhoneNumber = 50;
		private static readonly byte _maxLengthCountryCode = 5;

		private PhoneNumberValue(string number, string countryCode)
		{
			Number = number;
			CountryCode = countryCode;
		}

		public string Number { get; init; }
		public string CountryCode { get; init; }

		public static ResultModel<PhoneNumberValue> Create(string phoneNumber, string countryCode)
		{
			if (!IsValidPhoneNumber(phoneNumber, countryCode))
			{
				return ResultModel<PhoneNumberValue>.Failure(_notValidPhoneNumberErrorMessage);
			}

			return ResultModel<PhoneNumberValue>.Success(new PhoneNumberValue(phoneNumber, countryCode));
		}

		private static bool IsValidPhoneNumber(string phoneNumber, string countryCode)
		{
			if (string.IsNullOrWhiteSpace(phoneNumber) ||
				string.IsNullOrWhiteSpace(countryCode) ||
				phoneNumber.Length > _maxLengthPhoneNumber ||
				countryCode.Length > _maxLengthCountryCode) { return false; }

			return Regex.IsMatch($"{countryCode}{phoneNumber}", _regexNumberValidation,
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
		}
	}
}
