using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Application.CurrentAccount.Services
{
	public interface ICurrentAccountAppService
	{
		ResultModel<CurrentAccountEntity> ToCurrentAccountEntity();
	}
}
