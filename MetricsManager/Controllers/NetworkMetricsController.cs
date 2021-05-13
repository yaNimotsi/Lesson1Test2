using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug("NLog встроен в NetworkMetricsController");
        }

        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkData([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get metrics NetworkMetricsController by interval {fromTime}-{toTime}");
            return Ok();
        }
    }
}
