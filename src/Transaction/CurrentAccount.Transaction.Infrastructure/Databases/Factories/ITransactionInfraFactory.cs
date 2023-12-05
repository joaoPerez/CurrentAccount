using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Transaction.Core.Transactions;
using CurrentAccount.Transaction.Infrastructure.Databases.Models;

namespace CurrentAccount.Transaction.Infrastructure.Databases.Factories
{
	public interface ITransactionInfraFactory
	{
		ResultModel<TransactionEntity> ToCurrentAccountEntity(TransactionDataModel accountDataModel);
	}
}
