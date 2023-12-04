namespace CurrentAccount.UnitTests.Core.Shared
{
	public class RecordedDateValueTests
	{
		[Fact]
		public void Create_ValidRecordedDateValue_ReturnsSuccess()
		{
			var validDate = DateTime.Now.AddDays(-1); // Set to a date in the past

			var result = RecordedDateValue.Create(validDate);
			
			Assert.True(result.IsSuccess);
			Assert.Equal(validDate, result.Value.RecordedDate);
		}

		[Fact]
		public void Create_InvalidRecordedDateValue_ReturnsFailure()
		{
			var futureDate = DateTime.Now.AddDays(1); // Set to a date in the future

			var result = RecordedDateValue.Create(futureDate);

			Assert.False(result.IsSuccess);
			Assert.Equal("The provided date time is invalid", result.ErrorMessage);
		}
	}
}
