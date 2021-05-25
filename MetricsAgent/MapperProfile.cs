using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<long, DateTimeOffset>().ConvertUsing(new DateTimeOffsetConverter());
            CreateMap<CpuMetrics, CpuMetricDto>();
<<<<<<< HEAD
=======
            CreateMap<HddMetrics, HddMetricDto>();
            CreateMap<NetworkMetrics, NetworkMetricDto>();
            CreateMap<DotNetMetrics, DotNetMetricDto>();
            CreateMap<RamMetrics, RamMetricDto>();
>>>>>>> 5bcb79fa37e7434e1666d9d6b8ade533f11ea327
        }
    }
}
