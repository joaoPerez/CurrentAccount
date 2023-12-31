﻿using CurrentAccount.Application.CurrentAccounts.Response;
using CurrentAccount.Application.Transactions;
using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Core.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentAccount.Application.CurrentAccounts.Handlers
{
	public class CustomerFullStatementHandler : ICustomerFullStatementHandler
	{
		private readonly ICurrentAccountService _currentAccountService;
		private readonly ICustomerService _customerService;
		private readonly ITransactionFromAccountGrpcService _transactionFromAccountGrpcService;

		private readonly string _findCustomerErrorMessage = "Customer not found";
		private readonly string _findAccountsErrorMessage = "No accounts found";

		public CustomerFullStatementHandler(ITransactionFromAccountGrpcService transactionFromAccountGrpcService, ICurrentAccountService currentAccountService, ICustomerService customerService)
		{
			_currentAccountService = currentAccountService;
			_customerService = customerService;
			_transactionFromAccountGrpcService = transactionFromAccountGrpcService;
		}

		public async Task<ResultModel<CustomerFullStatementResponseModel>> HandleCustomerFullStatement(Guid customerId)
		{
			var customer = await _customerService.GetCustomerById(customerId);

			if (customer == null)
			{
				ResultModel<CustomerFullStatementResponseModel>.Failure(_findCustomerErrorMessage);
			}

			var currentAccounts = await _currentAccountService.GetCurrentAccountsFromCustomer(customerId);

			if (currentAccounts == null || currentAccounts.Count == 0)
			{
				ResultModel<CustomerFullStatementResponseModel>.Failure(_findAccountsErrorMessage);
			}

			var transactions = new List<TransactionResponseModel>(currentAccounts.Count);

			foreach (var account in currentAccounts)
			{
				var transactionsResult = await _transactionFromAccountGrpcService.GetTransactionsFromAccount(account);

				if(!transactionsResult.IsSuccess)
				{
					return ResultModel<CustomerFullStatementResponseModel>.Failure(transactionsResult.ErrorMessage);
				}

				transactions.AddRange(transactionsResult.Value);
			}

			decimal balance = 0;

			if (transactions != null && transactions.Count > 0)
			{
				balance = transactions.OrderByDescending(t => t.TransactionDate).Select(t => t.ActualBalance).First();
			}

			// Considering only individual customer for this example
			var individualCustomer = customer.Value as IndividualCustomerEntity;

			var response = new CustomerFullStatementResponseModel(individualCustomer.FirstName.Name,
																  individualCustomer.LastName.Name,
																  balance,
																  transactions);

			return ResultModel<CustomerFullStatementResponseModel>.Success(response);
		}
	}
}
