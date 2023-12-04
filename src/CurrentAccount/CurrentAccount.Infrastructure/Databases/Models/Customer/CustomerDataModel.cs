using System.ComponentModel.DataAnnotations;

namespace CurrentAccount.Infrastructure.Database.Models.Customer
{
    public class CustomerDataModel
    {
        [Key]
        public Guid CustomerId { get; set; }
    }
}
