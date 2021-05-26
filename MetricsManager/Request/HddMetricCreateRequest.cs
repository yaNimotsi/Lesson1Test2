using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL.Requests
{
    public class HddMetricCreateRequest
    {
        public string AgentPath { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
