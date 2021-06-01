using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;

using Quartz;

using System;
using System.Threading.Tasks;
using MetricsManager.Client.Interface;
using MetricsManager.Client.Request;

namespace MetricsManager.DAL.Jobs.MetricJobs
{
    public class HddMetricsJob : IJob
    {
        //physical disk
        private readonly IHddMetricsRepository _hddRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _client;
        public HddMetricsJob(IHddMetricsRepository repository, IAgentsRepository agentsRepository, IMetricsAgentClient client)
        {
            _hddRepository = repository;
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
                var fromTime = _hddRepository.GetMaxDate(agentId);
                var toTime = DateTimeOffset.UtcNow;

                var allMetrics = _client.GetHddMetricsFromAgent(new AllHddMetricsApiRequest()
                {
                    AgentUri = agentUri,
                    FromTime = fromTime,
                    ToTime = toTime
                });

                if (allMetrics.Metrics.Count <= 0) continue;
                foreach (var metric in allMetrics.Metrics)
                {
                    _hddRepository.Create(new HddMetrics()
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
