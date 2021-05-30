using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Client.Response
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetricApiDto> Metrics { get; set; }
    }

    public class CpuMetricApiDto
    {
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
