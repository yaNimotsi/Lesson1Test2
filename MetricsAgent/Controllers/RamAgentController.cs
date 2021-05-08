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
    public class RamAgentController : ControllerBase
    {
        private readonly ILogger<RamAgentController> _logger;
        private IRamMetricsRepository repository;

        public RamAgentController(ILogger<RamAgentController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug("NLog in NetworkAgentController");
            this.repository = repository;
        }

        [HttpPost("post")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation("Start method Create in CpuAgentController");
            repository.Create(new RamMetrics()
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

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto { Time = Converter.ConvertToLong(metric.Time), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }


        [HttpGet("available")]
        public IActionResult GetFreeSpaceRum()
        {
            _logger.LogInformation("Start method GetFreeSpaceRum in RamAgentController");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeRamForPeriodOfTime([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Start method GetFreeRamForPeriodOfTime in RamAgentController by interval {fromTime}-{toTime}");
            return Ok();
        }
    }
}
