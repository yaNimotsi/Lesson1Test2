using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using Quartz;

namespace MetricsAgent.DAL.Jobs.Jobs
{
    public class NetworkMetricsJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly PerformanceCounter _networkCounter;
        public NetworkMetricsJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network Interface", "% Bytes Sent/sec");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_networkCounter.NextValue());
            var time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            _repository.Create(new NetworkMetrics { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
