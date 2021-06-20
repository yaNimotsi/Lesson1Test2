using AutoMapper;

using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;

using System;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<long, DateTimeOffset>().ConvertUsing(new DateTimeOffsetConverter());
            CreateMap<CpuMetrics, CpuMetricDto>();
            CreateMap<DotNetMetrics, DotNetMetricDto>();
            CreateMap<HddMetrics, HddMetricDto>();
            CreateMap<NetworkMetrics, NetworkMetricDto>();
            CreateMap<RamMetrics, RamMetricDto>();
        }
    }
}
