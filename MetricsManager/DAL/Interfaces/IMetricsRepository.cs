using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IMetricsRepository<T> where T: class
    {
        List<T> GetByTimePeriod(DateTimeOffset item, DateTimeOffset item2);
        void Create(T item);
        
    }
}
