using Microsoft.AspNetCore.Mvc;

using System;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkAgentController : ControllerBase
    {
        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkData([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
