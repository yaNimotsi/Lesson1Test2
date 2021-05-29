using System;

namespace MetricsManager.Request
{
    public class CpuMetricCreateRequest
    {
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
