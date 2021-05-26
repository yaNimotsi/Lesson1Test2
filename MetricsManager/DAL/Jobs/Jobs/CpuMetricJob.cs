using System;
using System.Threading.Tasks;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;
using Quartz;

namespace MetricsManager.DAL.Jobs.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;
        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            
            _repository.Create(new CpuMetrics{Time = time, Value = cpuUsageInPercents});

            return Task.CompletedTask;
        }
    }
}
