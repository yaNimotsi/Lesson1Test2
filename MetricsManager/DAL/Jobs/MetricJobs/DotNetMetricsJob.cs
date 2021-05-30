using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;

using Quartz;

using System;
using System.Threading.Tasks;
using MetricsManager.Client.Interface;
using MetricsManager.Client.Request;

namespace MetricsManager.DAL.Jobs.MetricJobs
{
    public class DotNetMetricsJob : IJob
    {
        private readonly IDotNetMetricsRepository _dotNetRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _client;
        public DotNetMetricsJob(IDotNetMetricsRepository repository, IAgentsRepository agentsRepository, IMetricsAgentClient client)
        {
            _dotNetRepository = repository;
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
                var fromTime = _dotNetRepository.GetMaxDate(agentId);
                var toTime = DateTimeOffset.UtcNow;

                var allMetrics = _client.GetDotNetMetricsFromAgent(new AllDotNetCpuMetricsApiRequest()
                {
                    AgentUri = agentUri,
                    FromTime = fromTime,
                    ToTime = toTime
                });

                if (allMetrics.Metrics.Count <= 0) continue;
                foreach (var metric in allMetrics.Metrics)
                {
                    _dotNetRepository.Create(new DotNetMetrics()
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
