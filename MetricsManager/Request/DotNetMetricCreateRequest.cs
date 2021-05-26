using System;
using System.Collections.Generic;

namespace MetricsManager.Request
{
    public class DotNetMetricCreateRequest
    {
        public string AgentPath { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
