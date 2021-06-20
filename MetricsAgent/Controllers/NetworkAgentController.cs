using MetricsAgent.DAL.Repository;
using MetricsAgent.Requests;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

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
            _logger.LogInformation($"Start method GetByTimePeriod in NetworkAgentController by interval {fromTime}-{toTime}");

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
