using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using MetricsAgent.Requests;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuAgentController : ControllerBase
    {
        private ICpuMetricsRepository repository;

        public CpuAgentController(ICpuMetricsRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("post")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            repository.Create(new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = repository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto {Time = metric.Time, Value = metric.Value, Id = metric.Id});
            }

            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsAgent([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
