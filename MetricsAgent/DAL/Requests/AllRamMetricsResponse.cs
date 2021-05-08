using System;
using System.Collections.Generic;

namespace MetricsAgent.Requests
{
    public class AllRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }

    public class RamMetricDto
    {
        public long Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
