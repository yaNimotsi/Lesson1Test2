using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        [HttpGet("left")]
        public IActionResult GetFreeDiskSpace()
        {
            return Ok();
        }
        [HttpGet("agentId/{agetnId}from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeDiskForPeriodOfTime([FromRoute] int agetnId,[FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
