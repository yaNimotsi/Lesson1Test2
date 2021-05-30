using MetricsManager.DAL.Client.Interface;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;

using Quartz;

using System;
using System.Threading.Tasks;
using MetricsManager.DAL.Client.Request;

namespace MetricsManager.DAL.Jobs.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _cpuRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _client;
        public CpuMetricJob(ICpuMetricsRepository cpuRepository, IAgentsRepository agentsRepository ,IMetricsAgentClient client)
        {
            _cpuRepository = cpuRepository;
            _agentsRepository = agentsRepository;
            _client = client;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var allAgents = _agentsRepository.GetAllAgents();

            foreach (var agent in allAgents)
            {
                var agentId = agent.AgentId;
                var agentUri = agent.AgentUrl;
                var fromTime = _cpuRepository.GetMaxDate();
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
