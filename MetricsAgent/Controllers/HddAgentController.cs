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
    public class HddAgentController : ControllerBase
    {
        private readonly ILogger<HddAgentController> _logger;
        private IHddMetricsRepository _repository;

        public HddAgentController(ILogger<HddAgentController> logger, IHddMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug("NLog in DotNetAgentController");
            this._repository = repository;
        }

        [HttpGet("byPeriod")]
        public IActionResult GetByTimePeriod([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Start method GetByTimePeriod in CpuAgentController by interval {fromTime}-{toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto() { Time = DateTimeOffset.FromUnixTimeMilliseconds(metric.Time).ToLocalTime(), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
