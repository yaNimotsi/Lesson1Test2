using Microsoft.AspNetCore.Mvc;

using System;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamAgentController : ControllerBase
    {
        private readonly ILogger<RamAgentController> _logger;

        public RamAgentController(ILogger<RamAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug("NLog in NetworkAgentController");
        }

        [HttpGet("available")]
        public IActionResult GetFreeSpaceRum()
        {
            _logger.LogInformation("Start method GetFreeSpaceRum in RamAgentController");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeRamForPeriodOfTime([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Start method GetFreeRamForPeriodOfTime in RamAgentController by interval {fromTime}-{toTime}");
            return Ok();
        }
    }
}
