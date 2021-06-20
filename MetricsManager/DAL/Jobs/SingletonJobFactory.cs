using Microsoft.Extensions.DependencyInjection;

using Quartz;
using Quartz.Spi;

using System;

namespace MetricsManager.DAL.Jobs
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var rez = _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
            return rez;
        }

        public void ReturnJob(IJob job)
        {
           
        }
    }
}
