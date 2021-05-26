using AutoMapper;

using MetricsManager.DAL.Models;
using MetricsManager.Response;

using System;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<long, DateTimeOffset>().ConvertUsing(new DateTimeOffsetConverterManager());
            CreateMap<CpuMetrics, CpuMetricDto>();
            CreateMap<CpuMetricDto,CpuMetrics>();
        }
    }
}
