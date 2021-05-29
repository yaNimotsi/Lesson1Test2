namespace MetricsManager.Request
{
    public class NetworkMetricCreateRequest
    {
        public int AgentId { get; set; }
        public long Time { get; set; }
        public int Value { get; set; }
    }
}
