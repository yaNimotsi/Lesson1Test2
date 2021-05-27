using MetricsAgent.DAL.Repository;
using MetricsAgent.Requests;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotNetAgentController : ControllerBase
    {
        private readonly ILogger<DotNetAgentController> _logger;
        private IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetAgentController(ILogger<DotNetAgentController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug("NLog in DotNetAgentController");
            this._repository = repository;
            _mapper = mapper;
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
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("MaxDate")]
        public IActionResult GetMaxDate()
        {
            _logger.LogInformation($"Start method GetMaxDate in DotNetAgentController");

            var metrics = _repository.GetMaxDate();

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
