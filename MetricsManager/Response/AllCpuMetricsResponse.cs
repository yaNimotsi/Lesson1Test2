using System;
using System.Collections.Generic;

namespace MetricsManager.Response
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }

    public class CpuMetricDto
    {
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
