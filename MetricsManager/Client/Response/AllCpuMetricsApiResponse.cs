using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Response
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetricApiDto> Metrics { get; set; }
    }

    public class CpuMetricApiDto
    {
        public int AgentId { get; set; }
        public DateTimeOffset time { get; set; }
        public int value { get; set; }
        public int id { get; set; }
    }
}
