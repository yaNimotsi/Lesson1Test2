using System;

namespace MetricsAgent.DAL.Requests
{
    public class HddMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}
