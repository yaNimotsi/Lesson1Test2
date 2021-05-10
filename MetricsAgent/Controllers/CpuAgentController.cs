using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using MetricsAgent.DAL.Requests;
using MetricsAgent.Requests;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuAgentController : ControllerBase
    {
        private readonly ILogger<CpuAgentController> _logger;
        private readonly ICpuMetricsRepository _repository;

        public CpuAgentController(ILogger<CpuAgentController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug("NLog in CpuAgentController");
            this._repository = repository;
        }
        
        [HttpGet("byPeriod")]
        public IActionResult GetByTimePeriod([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Start method GetByTimePeriod in CpuAgentController by interval {fromTime}-{toTime}");
            
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Time = DateTimeOffset.FromUnixTimeMilliseconds(metric.Time).ToLocalTime(), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
