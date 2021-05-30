namespace MetricsManager.DAL.Models
{
    public class HddMetrics
    {
        public long Id { get; set; }
        public long AgentId { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
