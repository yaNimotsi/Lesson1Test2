using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Response
{
    public class AllNetworkMetricsApiResponse
    {
        public List<NetworkMetricApiDto> Metrics { get; set; }
    }

    public class NetworkMetricApiDto
    {
        public int AgentId { get; set; }
        public DateTimeOffset time { get; set; }
        public int value { get; set; }
        public int id { get; set; }
    }
}
