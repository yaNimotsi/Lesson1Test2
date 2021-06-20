using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgents<T> where T: class
    {
        List<T> GetAllAgents();
    }
}
