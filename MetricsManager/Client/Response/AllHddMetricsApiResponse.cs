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
        public DateTimeOffset time { get; set; }
        public int value { get; set; }
        public int id { get; set; }
    }
}
