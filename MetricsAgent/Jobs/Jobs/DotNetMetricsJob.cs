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
    public class DotNetMetricsJob
    {
        private readonly IDotNetMetricsRepository _repository;
        private PerformanceCounter _dotNetCounter;
        public DotNetMetricsJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_dotNetCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new DotNetMetrics { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
