namespace CurrentAccount.Application.CurrentAccount
{
	public record CreateCurrentAccountCommand(Guid CustomerId, decimal InitialCredit);
}
