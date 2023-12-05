using System.ComponentModel.DataAnnotations;

namespace CurrentAccount.Infrastructure.Database.Models.Customer
{
    public class IndividualCustomerDataModel : CustomerDataModel
    {
		[MaxLength(255)]
		public string FirstName { get; set; }
		[MaxLength(255)]
		public string LastName { get; set; }
    }
}
