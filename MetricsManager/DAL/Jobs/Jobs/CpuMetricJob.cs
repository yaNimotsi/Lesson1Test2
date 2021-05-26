using System;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;
using MetricsManager.Request;
using MetricsManager.Response;
using Microsoft.AspNetCore.Authentication;
using Quartz;

namespace MetricsManager.DAL.Jobs.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsManagerRepository _repository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricAgentClient _metricAgentClient;
        private readonly IMapper _mapper;
        public CpuMetricJob(ICpuMetricsManagerRepository repository, IAgentsRepository agentsRepository, IMetricAgentClient metricAgentClient, IMapper mapper)
        {
            _repository = repository;
            _agentsRepository = agentsRepository;
            _metricAgentClient = metricAgentClient;
            _mapper = mapper;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var allAgents = _agentsRepository.GetAllAgent();

            foreach (var agent in allAgents)
            {
                var agentUri = agent.AgentUrl;
                var time = DateTimeOffset.UtcNow;

                var allCpuMetrics = _metricAgentClient.GetAllCpuMetricsResponse(new CpuMetricCreateRequest
                {
                    AgentPath = agentUri,
                    FromTime = time,
                    ToTime = time
                });

                foreach (var cpuMetric in allCpuMetrics.Metrics)
                {
                    _repository.Create(_mapper.Map<CpuMetrics>(cpuMetric));
                }

                //Строка для получения максимального значения даты в имеющейся таблице
                //SELECT id, value, time FROM CpuMetrics WHERE time = (SELECT max(time) FROM CpuMetrics)
            }
            return Task.CompletedTask;
        }
    }
}
