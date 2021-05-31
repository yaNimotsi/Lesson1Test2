using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using Quartz;

namespace MetricsAgent.DAL.Jobs.Jobs
{
    public class HddMetricsJob : IJob
    {
        //physical disk
        private readonly IHddMetricsRepository _repository;
        private readonly PerformanceCounter _hddCounter;
        public HddMetricsJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "Disk Reads/sec","_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            _repository.Create(new HddMetrics { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
