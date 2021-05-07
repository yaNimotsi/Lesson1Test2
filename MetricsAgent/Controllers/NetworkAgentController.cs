using Microsoft.AspNetCore.Mvc;

using System;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkAgentController : ControllerBase
    {
        private readonly ILogger<NetworkAgentController> _logger;

        public NetworkAgentController(ILogger<NetworkAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug("NLog in NetworkAgentController");
        }

        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkData([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Start method GetNetworkData in NetworkAgentController by interval {fromTime}-{toTime}");
            return Ok();
        }
    }
}
