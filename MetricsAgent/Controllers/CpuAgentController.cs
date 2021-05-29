using AutoMapper;

using MetricsAgent.DAL.Repository;
using MetricsAgent.Requests;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuAgentController : ControllerBase
    {
        private readonly ILogger<CpuAgentController> _logger;
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper mapper;

        public CpuAgentController(ILogger<CpuAgentController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug("NLog in CpuAgentController");
            this._repository = repository;
            this.mapper = mapper;
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
                response.Metrics.Add(mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}