using System;
using System.Collections.Generic;

namespace MetricsAgent.Requests
{
    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }

    public class DotNetMetricDto
    {
        public long Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
