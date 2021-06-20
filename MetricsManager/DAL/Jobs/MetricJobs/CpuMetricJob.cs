using MetricsManager.Client.Interface;
using MetricsManager.Client.Request;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;

using Microsoft.Extensions.Logging;

using Quartz;

using System;
using System.Threading.Tasks;

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
            _logger.Log(LogLevel.Debug,"Start CpuMetricJob");

            var allAgents = _agentsRepository.GetAllAgents();

            //Если агентов в БД не обнаруженно, выходим из Job
            if (allAgents.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var agent in allAgents)
            {
                var agentId = agent.AgentId;
                var agentUri = agent.AgentUri;
                var fromTime = _cpuRepository.GetMaxDate(agentId);
                var toTime = DateTimeOffset.UtcNow;

                var allMetrics = _client.GetCpuMetricsFromAgent(new AllCpuMetricsApiRequest
                {
                    AgentUri = agentUri,
                    FromTime = fromTime,
                    ToTime = toTime
                });

                if (allMetrics == null || allMetrics?.Metrics.Count == 0) continue;

                foreach (var metric in allMetrics.Metrics)
                {
                    _cpuRepository.Create( new CpuMetrics
                    {
                        AgentId = agentId,
                        Id = metric.id,
                        Time = metric.time.ToUnixTimeMilliseconds(),
                        Value = metric.value
                    });
                }
            }
            return Task.CompletedTask;
        }
    }
}
