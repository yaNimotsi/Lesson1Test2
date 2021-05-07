using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug("NLog подключен к HddMetricsController");
        }

        [HttpGet("left")]
        public IActionResult GetFreeDiskSpace()
        {
            _logger.LogInformation("Get free space on Hdd");
            return Ok();
        }
        [HttpGet("agentId/{agentId}from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeDiskForPeriodOfTime([FromRoute] int agentId,[FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get free space on Hdd by agent {agentId}");
            return Ok();
        }
    }
}
