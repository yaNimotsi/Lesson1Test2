using System;

namespace MetricsAgent.DAL.Requests
{
    public class DotNetMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}
