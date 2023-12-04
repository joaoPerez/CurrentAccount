using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CurrentAccount.Infrastructure.Database.Models.Customer;

namespace CurrentAccount.Infrastructure.Database.Models.CurrentAccount
{
    public class CurrentAccountDataModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public byte AccountDigit { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        [Required]
        public decimal Balance { get; set; } = 0;

        [Required]
        public string Currency { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now.ToUniversalTime();

#nullable enable
        public DateTime? ClosingDate { get; set; } = null;

        public Guid CustomerId { get; set; }

        [Required]
        public CustomerDataModel Customer { get; set; }

		public Guid AccountHolderAddressId { get; set; }

		public AccountHolderAddressDataModel AccountHolderAddress { get; set; }

		public Guid ContactInfoId { get; set; }

		public ContactInformationDataModel ContactInfo { get; set; }
	}
}
