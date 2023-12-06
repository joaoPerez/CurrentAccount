﻿namespace CurrentAccount.Application.CurrentAccounts.Response
{
	public record TransactionEntityResponseModel(Guid TransactionId, 
											     Guid AccountId, 
												 DateTime TransactionDate, 
												 string Type, 
												 decimal Amount, 
												 string Description, 
												 decimal ActualBalance, 
												 string Currency);
}
