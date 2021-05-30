using System;
using System.Threading.Tasks;
using MetricsManager.Client.Interface;
using MetricsManager.Client.Request;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;
using Microsoft.Extensions.Logging;
using Quartz;

namespace MetricsManager.DAL.Jobs.MetricJobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _cpuRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _client;
        private readonly ILogger<CpuMetricJob> _logger;
        public CpuMetricJob(ICpuMetricsRepository cpuRepository, IAgentsRepository agentsRepository, IMetricsAgentClient client, ILogger<CpuMetricJob> logger)
        {
            _cpuRepository = cpuRepository;
            _agentsRepository = agentsRepository;
            _client = client;
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var allAgents = _agentsRepository.GetAllAgents();

            foreach (var agent in allAgents)
            {
                var agentId = agent.AgentId;
                var agentUri = agent.AgentUrl;
                var fromTime = _cpuRepository.GetMaxDate(agentId);
                var toTime = DateTimeOffset.UtcNow;

                var allMetrics = _client.GetCpuMetricsFromAgent(new AllCpuMetricsApiRequest
                {
                    AgentUri = agentUri,
                    FromTime = fromTime,
                    ToTime = toTime
                });

                if (allMetrics.Metrics.Count <= 0) continue;

                foreach (var metric in allMetrics.Metrics)
                {
                    _cpuRepository.Create( new CpuMetrics
                    {
                        AgentId = agentId,
                        Id = metric.Id,
                        Time = metric.Time.ToUnixTimeMilliseconds(),
                        Value = metric.Value
                    });
                }
            }
            return Task.CompletedTask;
        }
    }
}
