using Microsoft.AspNetCore.Mvc;

using System;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HddAgentController : ControllerBase
    {
        private readonly ILogger<HddAgentController> _logger;

        public HddAgentController(ILogger<HddAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug("NLog in DotNetAgentController");
        }

        [HttpGet("left")]
        public IActionResult GetFreeDiskSpace()
        {
            _logger.LogInformation("Start method GetFreeDiskSpace in HddAgentController");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeDiskForPeriodOfTime([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Start method GetFreeDiskForPeriodOfTime in HddAgentController by interval {fromTime}-{toTime}");
            return Ok();
        }
    }
}
