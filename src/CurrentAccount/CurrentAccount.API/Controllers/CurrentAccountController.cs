using CurrentAccount.Application.CurrentAccount;
using CurrentAccount.Application.CurrentAccount.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CurrentAccount.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CurrentAccountController : ControllerBase
	{
		private readonly ILogger<HealthCheckController> _logger;
		private readonly ICreateCurrentAccountHandler _createCurrentAccountHandler;
		public CurrentAccountController(ILogger<HealthCheckController> logger, ICreateCurrentAccountHandler createCurrentAccountHandler)
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
				return BadRequest(result.ErrorMessage);
			}

			return Ok(result.Value);
		}
	}
}