using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.UnitTests.Core.Shared.Result
{
	public class ResultModelTests
	{
		[Fact]
		public void NewResult_Success_OutputRightValues()
		{
			var successMessage = "Any value";

			var result = ResultModel<string>.Success(successMessage);

			Assert.True(result.IsSuccess);
			Assert.Equal(successMessage, result.Value);
			Assert.Null(result.ErrorMessage);
		}

		[Fact]
		public void NewResult_Failure_OutputRightValues()
		{
			var errorMessage = "Any error";

			var result = ResultModel<string>.Failure(errorMessage);

			Assert.False(result.IsSuccess);
			Assert.Null(result.Value);
			Assert.Equal(errorMessage, result.ErrorMessage);
		}
	}
}
