namespace MetricsManager.DAL.Models
{
    public class CpuMetrics
    {
        public long Id { get; set; }
        public long AgentId { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
