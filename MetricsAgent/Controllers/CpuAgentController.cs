using Microsoft.AspNetCore.Mvc;

using System;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuAgentController : ControllerBase
    {
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgetnt([FromRoute] int agentId, [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

        [HttpGet("claster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllClaster([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
