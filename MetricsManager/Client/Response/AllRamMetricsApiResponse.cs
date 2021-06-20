using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Response
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetricApiDto> Metrics { get; set; }
    }

    public class RamMetricApiDto
    {
        public int AgentId { get; set; }
        public DateTimeOffset time { get; set; }
        public int value { get; set; }
        public int id { get; set; }
    }
}
