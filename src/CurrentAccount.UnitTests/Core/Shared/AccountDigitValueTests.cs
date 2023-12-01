namespace CurrentAccount.UnitTests.Core.Shared
{
    public class AccountDigitValueTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(9)]
        public void Create_ValidAccountDigit_ReturnsSuccess(byte accountDigit)
        {
            var result = AccountDigitValue.Create(accountDigit);

            Assert.True(result.IsSuccess);
            Assert.Equal(accountDigit, result.Value.AccountDigit);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(unchecked((byte)-5))] // Checking with forced negative value
        public void Create_InvalidAccountDigit_ReturnsFailure(byte accountDigit)
        {
            var result = AccountDigitValue.Create(accountDigit);

            Assert.False(result.IsSuccess);
            Assert.Equal("The provided account digit is not valid", result.ErrorMessage);
        }
    }
}
