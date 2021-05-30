using System;
using System.Collections.Generic;
using System.Configuration.Internal;

namespace MetricsManager.DAL.Interfaces
{
    public interface IMetricsRepository<T> where T: class
    {
        List<T> GetByTimePeriod(DateTimeOffset item, DateTimeOffset item2);
        List<T> GetByAgentAndTimePeriod(int agentId, DateTimeOffset item, DateTimeOffset item2);

        DateTimeOffset GetMaxDate(long agentId);
        void Create(T item);
        
    }
}
