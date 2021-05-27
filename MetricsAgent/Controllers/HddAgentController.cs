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
    public class HddAgentController : ControllerBase
    {
        private readonly ILogger<HddAgentController> _logger;
        private IHddMetricsRepository _repository;
        private readonly IMapper _mapper;

        public HddAgentController(ILogger<HddAgentController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug("NLog in HddAgentController");
            this._repository = repository;
            _mapper = mapper;
        }

        [HttpGet("byPeriod")]
        public IActionResult GetByTimePeriod([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Start method GetByTimePeriod in HddAgentController by interval {fromTime}-{toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

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

        [HttpGet("MaxDate")]
        public IActionResult GetMaxDate()
        {
            _logger.LogInformation($"Start method GetMaxDate in HddAgentController");

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
