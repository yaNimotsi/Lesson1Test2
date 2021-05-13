using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    static class Converter
    {
        public static long ConvertToLong(TimeSpan valueToConvert)
        {
            return valueToConvert.TotalSeconds > 0 ? Convert.ToInt64(valueToConvert.TotalSeconds) : 0;
        }

        public static TimeSpan ConvertToTimeSpan(long valueToConvert)
        {
            return valueToConvert > 0 ? new TimeSpan(valueToConvert * 10000000) : TimeSpan.Zero;
        }
    }
}
