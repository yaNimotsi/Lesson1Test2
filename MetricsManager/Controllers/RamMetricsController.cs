using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug("NLog встроен в RamMetricsController");
        }

        [HttpGet("available")]
        public IActionResult GetFreeSpaceRum()
        {
            _logger.LogInformation($"Get free space Ram");
            return Ok();
        }
        [HttpGet("agentId/{agetnId}from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeRamForPeriodOfTime([FromRoute] int agetnId, [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get free space Ram by interval {fromTime}-{toTime}");
            return Ok();
        }
    }
}
