using System;

namespace MetricsManager.DAL.Models
{
    public class CpuMetrics
    {
        public long AgentId { get; set; }
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
