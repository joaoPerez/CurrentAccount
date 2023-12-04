namespace CurrentAccount.UnitTests.Core.Shared
{
    public class PhoneNumberValueTests
	{
		[Theory]
		[InlineData("1234567890", "1")]
		[InlineData("9876543210", "44")]
		[InlineData("12345678901234567890123456789012345678901234567890", "12345")]
		public void Create_ValidPhoneNumber_ReturnsSuccess(string phoneNumber, string countryCode)
		{
			var result = PhoneNumberValue.Create(phoneNumber, countryCode);

			Assert.True(result.IsSuccess);
			Assert.Equal(phoneNumber, result.Value.Number);
			Assert.Equal(countryCode, result.Value.CountryCode);
		}

		[Theory]
		[InlineData(null, "1")]
		[InlineData("555-1234", "1")]
		[InlineData("555-1234", null)]
		[InlineData("invalid", "invalid")]
		public void Create_InvalidPhoneNumber_ReturnsFailure(string phoneNumber, string countryCode)
		{
			var result = PhoneNumberValue.Create(phoneNumber, countryCode);

			Assert.False(result.IsSuccess);
			Assert.Equal("The provided phone number is not valid", result.ErrorMessage);
		}
	}

}
