using CurrentAccount.Transaction.Application.Transactions.Commands;
using CurrentAccount.Transaction.Application.Transactions.Handlers;
using CurrentAccount.Transaction.Core.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace CurrentAccount.Transaction.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]	
	public class TransactionController : ControllerBase
	{
		private readonly ILogger<TransactionController> _logger;
		private readonly ICreateTransactionHandler _createTransactionHandler;
		private readonly ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, ICreateTransactionHandler createTransactionHandler, ITransactionService transactionService)
        {
			_logger = logger;
            _createTransactionHandler = createTransactionHandler;
			_transactionService = transactionService;
        }

        [HttpGet]
		[Route("GetTransactionsFromAccount")]
		public async Task<IActionResult> GetAllTransactionsByAccountAsync(Guid accountId)
		{

			var transactionsResult = await _transactionService.GetAllTransactionsFromAccount(accountId);

			if(!transactionsResult.IsSuccess)
			{
				_logger.LogError(transactionsResult.ErrorMessage, transactionsResult);
				return BadRequest(transactionsResult.ErrorMessage);
			}

			return Ok(transactionsResult.Value);
		}

		[HttpPost]
		[Route("CreateTransaction")]
		public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
		{
			var result = await _createTransactionHandler.HandleCreateTransaction(command);

			if(!result.IsSuccess)
			{
				_logger.LogError(result.ErrorMessage, result);
				return BadRequest(result.ErrorMessage);
			}

			return Ok(result.Value);
		}
	}
}
