namespace CurrentAccount.UnitTests.Core.Shared
{
    public class AccountNumberValueTests
    {
        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        [InlineData("0000000000")]
        public void Create_ValidAccountNumber_ReturnsSuccess(string accountNumber)
        {
            var result = AccountNumberValue.Create(accountNumber);

            Assert.True(result.IsSuccess);
            Assert.Equal(accountNumber, result.Value.AccountNumber);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12345")]
        [InlineData("12345678901")]
        [InlineData("12a3456789")]
        [InlineData(" 123456789")]
        [InlineData("123456789 ")]
        [InlineData("12-34567890")]
        [InlineData("12 34567890")]
        [InlineData("12.34567890")]
        [InlineData("-1234567890")]
        [InlineData("-123456789")]
        public void Create_InvalidAccountNumber_ReturnsFailure(string accountNumber)
        {
            var result = AccountNumberValue.Create(accountNumber);

            Assert.False(result.IsSuccess);
            Assert.Equal("The provided account number is not valid", result.ErrorMessage);
        }
    }

}
