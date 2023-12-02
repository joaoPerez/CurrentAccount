using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Application.CurrentAccount.Handlers
{
    public interface ICreateCurrentAccountHandler
    {
        Task<ResultModel<Guid>> HandleExistentCustomer(CreateCurrentAccountCommand command);
    }
}
