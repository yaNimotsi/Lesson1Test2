using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotNet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCount([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogDebug($"Получение метрик DotNetController за промежуток времени {fromTime}-{toTime}");
            return Ok();
        }
    }
}
