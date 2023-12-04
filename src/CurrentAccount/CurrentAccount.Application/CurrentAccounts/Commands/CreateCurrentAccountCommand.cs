namespace CurrentAccount.Application.CurrentAccounts.Commands
{
    public record CreateCurrentAccountCommand(Guid CustomerId, decimal InitialCredit);
}
