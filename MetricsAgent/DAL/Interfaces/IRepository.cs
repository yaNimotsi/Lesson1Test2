using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public interface IRepository<T> where T: class
    {
        List<T> GetByTimePeriod(DateTimeOffset item, DateTimeOffset item2);
        void Create(T item);
        
    }
}
