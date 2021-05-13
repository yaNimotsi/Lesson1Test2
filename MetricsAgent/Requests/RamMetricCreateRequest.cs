using System;

namespace MetricsAgent.DAL.Requests
{
    public class RamMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}
