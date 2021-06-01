using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Response
{
    public class AllDotNetMetricsApiResponse
    {
        public List<DotNetMetricApiDto> Metrics { get; set; }
    }

    public class DotNetMetricApiDto
    {
        public int AgentId { get; set; }
        public DateTimeOffset time { get; set; }
        public int value { get; set; }
        public int id { get; set; }
    }
}
