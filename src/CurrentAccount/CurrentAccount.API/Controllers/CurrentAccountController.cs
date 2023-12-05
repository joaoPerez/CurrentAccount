using CurrentAccount.Application.CurrentAccount.Handlers;
using CurrentAccount.Application.CurrentAccounts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CurrentAccount.API.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class CurrentAccountController : ControllerBase
	{
		private readonly ILogger<CurrentAccountController> _logger;
		private readonly ICreateCurrentAccountHandler _createCurrentAccountHandler;

		public CurrentAccountController(ILogger<CurrentAccountController> logger, ICreateCurrentAccountHandler createCurrentAccountHandler)
		{
			_logger = logger;
			_createCurrentAccountHandler = createCurrentAccountHandler;
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateAccountAsync(CreateCurrentAccountCommand accountCommand)
		{
			var result = await _createCurrentAccountHandler.HandleExistentCustomer(accountCommand);

			if (!result.IsSuccess)
			{
				_logger.LogError(result.ErrorMessage);
				return BadRequest(result.ErrorMessage);
			}

			return Ok(result.Value);
		}
	}
}