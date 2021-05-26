using System;

namespace MetricsManager.DAL.Models
{
    public class CpuMetrics
    {
        public int AgentId { get; set; }
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
