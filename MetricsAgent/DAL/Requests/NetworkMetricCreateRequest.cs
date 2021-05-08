using System;

namespace MetricsAgent.DAL.Requests
{
    public class NetworkMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}
