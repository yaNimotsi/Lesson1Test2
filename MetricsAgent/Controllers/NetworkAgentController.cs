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
        private INetworkMetricsRepository repository;

        public NetworkAgentController(ILogger<NetworkAgentController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug("NLog in NetworkAgentController");
            this.repository = repository;
        }

        [HttpPost("post")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation("Start method Create in CpuAgentController");
            repository.Create(new NetworkMetrics()
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

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto { Time = Converter.ConvertToLong(metric.Time), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }


        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkData([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Start method GetNetworkData in NetworkAgentController by interval {fromTime}-{toTime}");
            return Ok();
        }
    }
}
