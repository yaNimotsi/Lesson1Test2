namespace MetricsManager.Request
{
    public class DotNetMetricCreateRequest
    {
        public int AgentId { get; set; }
        public long Time { get; set; }
        public int Value { get; set; }
    }
}
