namespace CurrentAccount.UnitTests.Core.Shared
{
	public class NameValueTests
	{
		private readonly string _nameValidationErrorMessage = "The provided name is not valid";

		[Fact]
		public void Create_ValidNameValue_ReturnsSuccess()
		{
			var validName = "Right Name";

			var result = NameValue.Create(validName);

			Assert.True(result.IsSuccess);
			Assert.Equal(validName, result.Value.Name);
		}

		[Fact]
		public void Create_TooLongName_ReturnsFailure()
		{
			var tooLongName = new string('A', 256); // Name greater than maximum length

			var result = NameValue.Create(tooLongName);

			Assert.False(result.IsSuccess);
			Assert.Equal(_nameValidationErrorMessage, result.ErrorMessage);
		}

		[Fact]
		public void Create_InvalidCharacters_ReturnsFailure()
		{
			var invalidName = "Wrong Name !"; // Invalid name

			var result = NameValue.Create(invalidName);

			Assert.False(result.IsSuccess);
			Assert.Equal(_nameValidationErrorMessage, result.ErrorMessage);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateInvalidNameValue_EmptyNullWhiteSpace_ReturnsFailure(string name)
		{
			var result = NameValue.Create(name);

			Assert.False(result.IsSuccess);
			Assert.Equal(_nameValidationErrorMessage, result.ErrorMessage);
		}
	}

}
