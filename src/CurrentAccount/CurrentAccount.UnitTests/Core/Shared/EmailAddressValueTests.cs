namespace CurrentAccount.UnitTests.Core.Shared
{
    public class EmailAddressValueTests
    {
        [Theory]
        [InlineData("right.email@example.com")]
        [InlineData("name.surname@gmail.com")]
        [InlineData("user@domain.co")]
        public void Create_ValidEmailAddress_ReturnsSuccess(string email)
        {
            var result = EmailAddressValue.Create(email);

            Assert.True(result.IsSuccess);
            Assert.Equal(email, result.Value.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("anytext")]
        [InlineData("user@domain@co")]
        [InlineData("user@domain.")]
        [InlineData("@domain.com")]
        [InlineData("user@.com")]
        [InlineData("user@domain.com@")]
        [InlineData("user@domain@.com")]
        [InlineData("user@dom@ain.com")]
        [InlineData("user@domain..com")]
        [InlineData("user@domain.c@m")]
        [InlineData("user@domain.12345678901234567890123456789012345678901234567890123456789012345678901234567890")] // 255 characters
        public void Create_InvalidEmailAddress_ReturnsFailure(string email)
        {
            var result = EmailAddressValue.Create(email);

            Assert.False(result.IsSuccess);
            Assert.Equal("The provided e-mail is not valid", result.ErrorMessage);
        }
    }

}
