using Microsoft.EntityFrameworkCore;

namespace CurrentAccount.Transaction.Infrastructure.Databases.Contexts
{
	public class TransactionContext : DbContext
	{
		public TransactionContext(DbContextOptions<TransactionContext> options)
		: base(options)
		{
		}


	}
}
