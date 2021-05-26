using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetByTimePeriodFromAgent(int agentId ,DateTimeOffset item, DateTimeOffset item2);
        List<T> GetByTimePeriod(DateTimeOffset item, DateTimeOffset item2);
        void Create(T item);

    }
}
