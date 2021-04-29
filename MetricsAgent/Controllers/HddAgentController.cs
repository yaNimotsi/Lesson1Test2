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
    }
}
