﻿using System;

namespace MetricsAgent.DAL.Requests
{
    public class NetworkMetricCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
