using System;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent;
using MetricsAgent.DAL.Requests;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;
using MetricsManager.Request;
using MetricsManager.Response;
using Microsoft.AspNetCore.Authentication;
using Quartz;

namespace MetricsManager.DAL.Jobs.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsManagerRepository _repository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricAgentClient _metricAgentClient;
        private readonly IMapper _mapper;
        public HddMetricJob(IHddMetricsManagerRepository repository, IAgentsRepository agentsRepository, IMetricAgentClient metricAgentClient, IMapper mapper)
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
                //Получение uri агента
                var agentUri = agent.AgentUrl;
                //Получение максимальной даты у агента
                var fromTime = _metricAgentClient.GetMaxDateHddMetricsInAgent(new HddMetricCreateRequest
                {
                    AgentUri = agentUri
                });
                //Дата, до которой будет собираться метрика
                var time = DateTimeOffset.UtcNow;

                var allCpuMetrics = _metricAgentClient.GetAllHddMetricsResponse(new HddMetricCreateRequest
                {
                    AgentUri = agentUri,
                    FromTime = DateTimeOffset.FromUnixTimeMilliseconds(fromTime.Time),
                    ToTime = time
                });

                foreach (var cpuMetric in allCpuMetrics.Metrics)
                {
                    _repository.Create(_mapper.Map<HddMetrics>(cpuMetric));
                }
            }
            return Task.CompletedTask;
        }
    }
}
