namespace CurrentAccount.UnitTests.Core.Shared
{
	public class DecimalNumberValueTests
	{
		[Theory]
		[InlineData(50.45)]
		[InlineData(0)]
		[InlineData(100000)]
		public void Create_ValidDecimalNumber_ReturnsSuccess(decimal value)
		{
			var result = DecimalNumberValue.Create(value);

			Assert.True(result.IsSuccess);
			Assert.Equal(value, result.Value.Value);
		}

		[Theory]
		[InlineData(-1, "The minimium value is 0")]
		[InlineData(100001, "The maximium value is 100000")]
		[InlineData(50.123, "The maximium decimal places are 2")]
		public void Create_InvalidDecimalNumber_ReturnsFailure(decimal value, string expectedErrorMessage)
		{
			var result = DecimalNumberValue.Create(value);

			Assert.False(result.IsSuccess);
			Assert.Equal(expectedErrorMessage, result.ErrorMessage);
		}
	}

}
