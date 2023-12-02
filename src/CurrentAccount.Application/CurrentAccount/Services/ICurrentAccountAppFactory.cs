using CurrentAccount.Application.CurrentAccount.Dto;
using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Application.CurrentAccount.Services
{
	public interface ICurrentAccountAppFactory
	{
		ResultModel<CurrentAccountEntity> ToCurrentAccountEntity(Guid accountUuid, CurrentAccountDto currentAccountDto, CustomerEntity customerEntity);
	}
}
