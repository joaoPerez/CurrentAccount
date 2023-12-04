using Microsoft.AspNetCore.Mvc;

namespace CurrentAccount.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HealthCheckController : ControllerBase
	{
		private readonly ILogger<HealthCheckController> _logger;

		public HealthCheckController(ILogger<HealthCheckController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		[Route("check")]
		public IActionResult Get()
		{
			return Ok("Running!");
		}
	}
}