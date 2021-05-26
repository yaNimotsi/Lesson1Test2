﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgentRepository<T> where T: class
    {
        List<T> GetAllAgent();
    }
}
