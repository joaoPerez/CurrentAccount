namespace CurrentAccount.UnitTests.Core.Shared
{
	public class CurrencyValueTests
	{
		[Theory]
		[InlineData("USD")]
		[InlineData("EUR")]
		public void Create_ValidCurrency_ReturnsSuccess(string currency)
		{
			var result = CurrencyValue.Create(currency);

			Assert.True(result.IsSuccess);
			Assert.Equal(currency, result.Value.Currency);
		}

		[Theory]
		[InlineData(null)]		
		[InlineData("")]
		[InlineData("UnknownCurrency")]
		public void Create_InvalidCurrency_ReturnsFailure(string currency)
		{
			var result = CurrencyValue.Create(currency);

			Assert.False(result.IsSuccess);
			Assert.Equal("The informed currency is invalid", result.ErrorMessage);
		}
	}

}
