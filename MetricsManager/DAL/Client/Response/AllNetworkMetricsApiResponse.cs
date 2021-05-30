using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Client.Response
{
    public class AllNetworkMetricsApiResponse
    {
        public List<NetworkMetricApiDto> Metrics { get; set; }
    }

    public class NetworkMetricApiDto
    {
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
