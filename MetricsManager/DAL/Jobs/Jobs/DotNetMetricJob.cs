using System;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.Repository;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;
using MetricsManager.Request;
using MetricsManager.Response;
using Microsoft.AspNetCore.Authentication;
using Quartz;

namespace MetricsManager.DAL.Jobs.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsManagerRepository _repository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricAgentClient _metricAgentClient;
        private readonly IMapper _mapper;
        public DotNetMetricJob(IDotNetMetricsManagerRepository repository, IAgentsRepository agentsRepository, IMetricAgentClient metricAgentClient, IMapper mapper)
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
                var fromTime = _metricAgentClient.GetMaxDateDotNetMetricsInAgent(new DotNetMetricCreateRequest()
                {
                    AgentUri = agentUri
                });
                //Дата, до которой будет собираться метрика
                var time = DateTimeOffset.UtcNow;

                var allCpuMetrics = _metricAgentClient.GetAllDotNetMetricsResponse(new DotNetMetricCreateRequest()
                {
                    AgentUri = agentUri,
                    FromTime = DateTimeOffset.FromUnixTimeMilliseconds(fromTime.Time),
                    ToTime = time
                });

                foreach (var cpuMetric in allCpuMetrics.Metrics)
                {
                    _repository.Create(_mapper.Map<DotNetMetrics>(cpuMetric));
                }
            }
            return Task.CompletedTask;
        }
    }
}
