using CurrentAccount.Core.Customer;

namespace CurrentAccount.UnitTests.Core.Customer
{
	public class BusinessTypeValueTests
	{
		private readonly string _businessTypeValueErrorMessage = "The informed business is invalid";
		
		[Theory]
		[InlineData("Individual")]
		[InlineData("SmallBusiness")]
		[InlineData("Corporation")]
		[InlineData("NonProfit")]
		public void Create_ValidBusinessType_ReturnsSuccess(string businessType)
		{
			var result = BusinessTypeValue.Create(businessType);

			Assert.True(result.IsSuccess);
			Assert.Equal(businessType, result.Value.Business);
		}

		[Theory]
		[InlineData("")]
		[InlineData("InvalidType")]
		public void Create_InvalidBusinessType_ReturnsFailure(string businessType)
		{
			var result = BusinessTypeValue.Create(businessType);

			Assert.False(result.IsSuccess);
			Assert.Equal(_businessTypeValueErrorMessage, result.ErrorMessage);
		}

		[Fact]
		public void Create_NullBusinessType_ReturnsFailure()
		{
			var result = BusinessTypeValue.Create(null);

			Assert.False(result.IsSuccess);
			Assert.Equal(_businessTypeValueErrorMessage, result.ErrorMessage);
		}
	}

}
