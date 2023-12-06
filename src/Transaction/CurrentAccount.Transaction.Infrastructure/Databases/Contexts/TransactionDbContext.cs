using CurrentAccount.Transaction.Infrastructure.Databases.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrentAccount.Transaction.Infrastructure.Databases.Contexts
{
	public class TransactionDbContext : DbContext
	{
		public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
		: base(options)
		{
		}

		public DbSet<TransactionDataModel> Transactions { get; set; } = null;
	}
}
