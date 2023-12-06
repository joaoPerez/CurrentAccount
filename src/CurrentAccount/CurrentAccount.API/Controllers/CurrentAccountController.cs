using CurrentAccount.Application.CurrentAccount.Handlers;
using CurrentAccount.Application.CurrentAccounts.Commands;
using CurrentAccount.Application.CurrentAccounts.Handlers;
using CurrentAccount.Application.CurrentAccounts.Response;
using Microsoft.AspNetCore.Mvc;

namespace CurrentAccount.API.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class CurrentAccountController : ControllerBase
	{
		private readonly ILogger<CurrentAccountController> _logger;
		private readonly ICreateCurrentAccountHandler _createCurrentAccountHandler;
		private readonly ICustomerFullStatementHandler _customerFullStatementHandler;

		public CurrentAccountController(ILogger<CurrentAccountController> logger, 
										ICreateCurrentAccountHandler createCurrentAccountHandler,
										ICustomerFullStatementHandler customerFullStatementHandler)
		{
			_logger = logger;
			_createCurrentAccountHandler = createCurrentAccountHandler;
			_customerFullStatementHandler = customerFullStatementHandler;
		}

		[HttpPost]
		[Route("create")]
		public async Task<ActionResult<Guid>> CreateAccountAsync(CreateCurrentAccountCommand accountCommand)
		{
			var result = await _createCurrentAccountHandler.HandleExistentCustomer(accountCommand);

			if (!result.IsSuccess)
			{
				_logger.LogError(result.ErrorMessage, result);
				return BadRequest(result.ErrorMessage);
			}

			return Ok(result.Value);
		}

		[HttpGet]
		[Route("customerFullStatement")]
		public async Task<ActionResult<CustomerFullStatementResponseModel>> GetCustomerFullStatement(Guid customerId)
		{
			var statementResult = await _customerFullStatementHandler.HandleCustomerFullStatement(customerId);

			if (!statementResult.IsSuccess)
			{
				return BadRequest(statementResult.ErrorMessage);
			}

			return Ok(statementResult.Value);
		}
	}
}