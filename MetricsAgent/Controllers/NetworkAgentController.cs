using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Requests;
using MetricsAgent.Requests;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkAgentController : ControllerBase
    {
        private readonly ILogger<NetworkAgentController> _logger;
        private readonly INetworkMetricsRepository _repository;

        public NetworkAgentController(ILogger<NetworkAgentController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug("NLog in NetworkAgentController");
            this._repository = repository;
        }

        [HttpGet("byPeriod")]
        public IActionResult GetByTimePeriod([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Start method GetByTimePeriod in CpuAgentController by interval {fromTime}-{toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto() { Time = DateTimeOffset.FromUnixTimeMilliseconds(metric.Time).ToLocalTime(), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
