using Microsoft.AspNetCore.Mvc;

using System;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Request;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMetricAgentClient _client;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IMetricAgentClient client)
        {
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в CpuMetricsController");

            _client = client;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Get metrics from agent id {agentId}");

            var metrics = _client.GetAllCpuMetricsResponse(
                new CpuMetricCreateRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime
                });
            return Ok(metrics);
        }
        
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all metrics from all agents");
            return Ok();
        }
    }
}
