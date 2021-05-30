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
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
