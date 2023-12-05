using System.ComponentModel.DataAnnotations;

namespace CurrentAccount.Infrastructure.Database.Models.Customer
{
    public class BusinessCustomerDataModel : CustomerDataModel
    {
		[MaxLength(255)]
		public string BusinessName { get; set; }
		[MaxLength(50)]
		public string BusinessType { get; set; }
    }
}
