namespace CurrentAccount.UnitTests.Core.Shared
{
    public class ZipCodeValueTests
    {
        [Theory]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("98765")]
        [InlineData("1111")]
        [InlineData("1234567890")]
        [InlineData("12345678901234567890")]
        public void Create_ValidZipCode_ReturnsSuccess(string zipCode)
        {
            var result = ZipCodeValue.Create(zipCode);

            Assert.True(result.IsSuccess);
            Assert.Equal(zipCode, result.Value.ZipCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123456789012345678901")]
        [InlineData("invalid")]
        [InlineData("1234a")]
        [InlineData("12345-67890")]
        [InlineData("12345-6789")]
        [InlineData("98765-4321")]
        [InlineData("1234-")]
        [InlineData("12345-67890a")]
        public void Create_InvalidZipCode_ReturnsFailure(string zipCode)
        {
            var result = ZipCodeValue.Create(zipCode);

            Assert.False(result.IsSuccess);
            Assert.Equal("The provided zip code is not valid", result.ErrorMessage);
        }
    }
}
