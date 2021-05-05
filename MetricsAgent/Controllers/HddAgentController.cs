using Microsoft.AspNetCore.Mvc;

using System;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HddAgentController : ControllerBase
    {
        [HttpGet("left")]
        public IActionResult GetFreeDiskSpace()
        {
            return Ok();
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeDiskForPeriodOfTime([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
