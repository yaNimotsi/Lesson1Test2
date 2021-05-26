using System;
using System.Collections.Generic;

namespace MetricsManager.Request
{
    public class CpuMetricCreateRequest
    {
        public string AgentUri { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
