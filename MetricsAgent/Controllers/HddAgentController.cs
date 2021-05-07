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
    public class HddAgentController : ControllerBase
    {
        private readonly ILogger<HddAgentController> _logger;
        private IHddMetricsRepository repository;

        public HddAgentController(ILogger<HddAgentController> logger, IHddMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug("NLog in DotNetAgentController");
            this.repository = repository;
        }

        [HttpPost("post")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation("Start method Create in CpuAgentController");
            repository.Create(new HddMetrics
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Start method GetAll in CpuAgentController");

            var metrics = repository.GetAll();

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpGet("left")]
        public IActionResult GetFreeDiskSpace()
        {
            _logger.LogInformation("Start method GetFreeDiskSpace in HddAgentController");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeDiskForPeriodOfTime([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Start method GetFreeDiskForPeriodOfTime in HddAgentController by interval {fromTime}-{toTime}");
            return Ok();
        }
    }
}
