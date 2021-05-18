using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using Quartz;

namespace MetricsAgent.Jobs.Jobs
{
    public class HddMetricsJob
    {
        //physical disk
        private readonly IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;
        public HddMetricsJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("Physical disk", "% Disk activity", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new HddMetrics { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
