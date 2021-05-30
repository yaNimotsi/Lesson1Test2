using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;

using Quartz;

using System;
using System.Threading.Tasks;
using MetricsManager.Client.Interface;
using MetricsManager.Client.Request;

namespace MetricsManager.DAL.Jobs.MetricJobs
{
    public class RamMetricsJob : IJob
    {
        private readonly IRamMetricsRepository _ramRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _client;
        
        public RamMetricsJob(IRamMetricsRepository repository, IAgentsRepository agentsRepository, IMetricsAgentClient client)
        {
            _ramRepository = repository;
            _agentsRepository = agentsRepository;
            _client = client;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var allAgents = _agentsRepository.GetAllAgents();

            foreach (var agent in allAgents)
            {
                var agentId = agent.AgentId;
                var agentUri = agent.AgentUri;
                var fromTime = _ramRepository.GetMaxDate(agentId);
                var toTime = DateTimeOffset.UtcNow;

                var allMetrics = _client.GetRamMetricsFromAgent(new AllRamMetricsApiRequest()
                {
                    AgentUri = agentUri,
                    FromTime = fromTime,
                    ToTime = toTime
                });

                if (allMetrics.Metrics.Count <= 0) continue;
                foreach (var metric in allMetrics.Metrics)
                {
                    _ramRepository.Create(new RamMetrics()
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
