using CurrentAccount.Application.CurrentAccounts.Response;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Application.CurrentAccounts.Handlers
{
	public interface ICustomerFullStatementHandler
	{
		Task<ResultModel<CustomerFullStatementResponseModel>> HandleCustomerFullStatement(Guid customerId);
	}
}
