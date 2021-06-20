using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using MetricsAgent.DAL.Requests;
using MetricsAgent.Requests;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotNetAgentController : ControllerBase
    {
        private readonly ILogger<DotNetAgentController> _logger;
        private IDotNetMetricsRepository _repository;

        public DotNetAgentController(ILogger<DotNetAgentController> logger, IDotNetMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug("NLog in DotNetAgentController");
            this._repository = repository;
        }
        [HttpGet("byPeriod")]
        public IActionResult GetByTimePeriod([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Start method GetByTimePeriod in DotNetAgentController by interval {fromTime}-{toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto { Time = DateTimeOffset.FromUnixTimeMilliseconds(metric.Time).ToLocalTime(), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
