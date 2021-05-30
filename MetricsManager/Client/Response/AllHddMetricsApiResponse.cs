using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Response
{
    public class AllHddMetricsApiResponse
    {
        public List<HddMetricApiDto> Metrics { get; set; }
    }

    public class HddMetricApiDto
    {
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
