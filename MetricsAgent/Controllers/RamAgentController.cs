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
    public class RamAgentController : ControllerBase
    {
        private readonly ILogger<RamAgentController> _logger;
        private readonly IRamMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RamAgentController(ILogger<RamAgentController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug("NLog in RamAgentController");
            this._repository = repository;
            _mapper = mapper;
        }

        [HttpGet("byPeriod")]
        public IActionResult GetByTimePeriod([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Start method GetByTimePeriod in RamAgentController by interval {fromTime}-{toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("MaxDate")]
        public IActionResult GetMaxDate()
        {
            _logger.LogInformation($"Start method GetMaxDate in RamAgentController");

            var metrics = _repository.GetMaxDate();

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
