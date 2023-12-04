namespace CurrentAccount.Infrastructure.Database.Models
{
	public class ContactInformationDataModel
	{
		public Guid Id { get; set; }
		public string PhoneNumber { get; set; }
		public string CountryCode { get; set; }
		public string Email { get; set; }
	}
}
