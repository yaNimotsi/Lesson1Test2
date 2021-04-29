using Microsoft.AspNetCore.Mvc;

using System;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamAgentController : ControllerBase
    {
        [HttpGet("available")]
        public IActionResult GetFreeSpaceRum()
        {
            return Ok();
        }
    }
}
