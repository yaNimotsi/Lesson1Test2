using System;
using System.Collections.Generic;

namespace MetricsManager.Request
{
    public class RamMetricCreateRequest
    {
        public string AgentPath { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
}
