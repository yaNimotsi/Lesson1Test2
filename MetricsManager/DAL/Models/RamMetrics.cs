namespace MetricsManager.DAL.Models
{
    public class RamMetrics
    {
        public long Id { get; set; }
        public long AgentId { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
