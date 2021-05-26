using System;
using AutoMapper;

namespace MetricsManager
{
    public class DateTimeOffsetConverterManager : ITypeConverter<long, DateTimeOffset>
    {
        public DateTimeOffset Convert(long source, DateTimeOffset destination, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(source);
        }

        public long Convert(DateTimeOffset source, long destination, ResolutionContext context)
        {
            return source.ToUnixTimeMilliseconds();
        }
    }
}
