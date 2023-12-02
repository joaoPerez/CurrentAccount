using CurrentAccount.Core.CurrentAccount;

namespace CurrentAccount.Application.CurrentAccount.Dto
{
	public class CurrentAccountDto
	{
		public string AccountNumber { get; set; }
		public byte AccountDigit { get; set; }
		public AccountTypeEnum AccountType { get; set; }
		public decimal Balance { get; set; }
		public string Currency { get; set; }
		public string Country { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string CountryCode { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? ClosingDate { get; set; }
	}
}
