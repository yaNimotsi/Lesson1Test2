using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using Quartz;

namespace MetricsAgent.DAL.Jobs.Jobs
{
    public class RamMetricsJob : IJob
    {
        private readonly IRamMetricsRepository _repository;
        private PerformanceCounter _rumCounter;
        public RamMetricsJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _rumCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_rumCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            _repository.Create(new RamMetrics() { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
