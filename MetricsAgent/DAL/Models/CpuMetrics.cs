﻿using System;

namespace MetricsAgent.DAL.Models
{
    public class CpuMetrics
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
