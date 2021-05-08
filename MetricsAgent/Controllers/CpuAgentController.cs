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
        private ICpuMetricsRepository repository;

        public CpuAgentController(ILogger<CpuAgentController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug("NLog in CpuAgentController");
            this.repository = repository;
        }

        [HttpPost("post")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation("Start method Create in CpuAgentController");
            repository.Create(new CpuMetric
            {
                Time = Converter.ConvertToTimeSpan(request.Time),
                Value = request.Value
            });
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Start method GetAll in CpuAgentController");

            var metrics = repository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto {Time = Converter.ConvertToLong(metric.Time), Value = metric.Value, Id = metric.Id});
            }

            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Start method GetMetricsAgent in CpuAgentController by interval {fromTime}-{toTime}");
            return Ok();
        }
    }
}
