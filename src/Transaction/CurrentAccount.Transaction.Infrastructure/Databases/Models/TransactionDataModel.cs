using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CurrentAccount.Transaction.Infrastructure.Databases.Models
{
	public class TransactionDataModel
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public Guid CurrentAccountId { get; set; }

		[Required]
		[MaxLength(50)]
		public string TransactionType { get; set; }

		[Required]
		[MaxLength(255)]
		public string Description { get; set; }

		[Column(TypeName = "decimal(6,2)")]
		[Required]
		public decimal ActualBalance { get; set; } = 0;

		[Column(TypeName = "decimal(6,2)")]
		[Required]
		public decimal Amount { get; set; }

		[Required]
		[MaxLength(50)]
		public string Currency { get; set; }

		[Required]
		public DateTime TransactionDate { get; set; } = DateTime.Now.ToUniversalTime();
	}
}
