using System;

namespace MetricsManager.DAL.Client.Request
{
    public class AllNetworkMetricsApiRequest
    {
        public string AgentUri { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
