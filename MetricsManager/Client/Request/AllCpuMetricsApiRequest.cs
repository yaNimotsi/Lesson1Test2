using System;

namespace MetricsManager.Client.Request
{
    public class AllCpuMetricsApiRequest
    {
        public string AgentUri { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
