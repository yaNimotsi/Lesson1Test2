using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Client.Response
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetricApiDto> Metrics { get; set; }
    }

    public class RamMetricApiDto
    {
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
