using Microsoft.AspNetCore.Mvc;

using System;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotNetAgentController : ControllerBase
    {
        private readonly ILogger<DotNetAgentController> _logger;

        public DotNetAgentController(ILogger<DotNetAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug("NLog in DotNetAgentController");
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCount([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Start method GetErrorsCount in DotNetAgentController");
            return Ok();
        }
    }
}
