using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Shared.Result;
using CurrentAccount.Infrastructure.Database.Models.CurrentAccount;

namespace CurrentAccount.Application.CurrentAccount.Services
{
	public interface ICurrentAccountInfraFactory
	{
		ResultModel<CurrentAccountEntity> ToCurrentAccountEntity(CurrentAccountDataModel accountDataModel);
	}
}
