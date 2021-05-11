﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MetricsAgent
{
    public class DateTimeOffsetConverter : ITypeConverter<long, DateTimeOffset>
    {
        public DateTimeOffset Convert(long source, DateTimeOffset destination, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(source);
        }
    }
}
