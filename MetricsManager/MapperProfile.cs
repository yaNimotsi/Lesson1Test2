using System;
using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Response;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<long, DateTimeOffset>().ConvertUsing(new DateTimeOffsetConverter());
            
            CreateMap<AgentModel, AgentDto>();
            
            CreateMap<CpuMetrics, CpuMetricDto>();
            CreateMap<DotNetMetrics, DotNetMetricDto>();
            CreateMap<HddMetrics, HddMetricDto>();
            CreateMap<NetworkMetrics, NetworkMetricDto>();
            CreateMap<RamMetrics, RamMetricDto>();
        }
    }
}
