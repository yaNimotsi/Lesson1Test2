using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public NetworkAgentController(ILogger<NetworkAgentController> logger, INetworkMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug("NLog in NetworkAgentController");
            this._repository = repository;
            _mapper = mapper;
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
                response.Metrics.Add( _mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("MaxDate")]
        public IActionResult GetMaxDate()
        {
            _logger.LogInformation($"Start method GetMaxDate in NetworkAgentController");

            var metrics = _repository.GetMaxDate();

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
