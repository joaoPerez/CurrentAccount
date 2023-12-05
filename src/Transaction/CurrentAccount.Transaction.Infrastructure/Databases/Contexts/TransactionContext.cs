using CurrentAccount.Transaction.Infrastructure.Databases.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrentAccount.Transaction.Infrastructure.Databases.Contexts
{
	public class TransactionContext : DbContext
	{
		public TransactionContext(DbContextOptions<TransactionContext> options)
		: base(options)
		{
		}

		public DbSet<TransactionDataModel> Transactions { get; set; } = null;
	}
}
