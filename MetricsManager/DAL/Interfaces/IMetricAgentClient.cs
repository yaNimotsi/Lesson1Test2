using MetricsAgent.DAL.Requests;

using MetricsManager.Request;
using MetricsManager.Response;

namespace MetricsManager.DAL.Interfaces
{
    public interface IMetricAgentClient
    {
        AllCpuMetricsResponse GetAllCpuMetricsResponse(CpuMetricCreateRequest request);
        AllHddMetricsResponse GetAllHddMetricsResponse(HddMetricCreateRequest request);
        AllNetworkMetricsResponse GetAllNetworkMetricsResponse(NetworkMetricCreateRequest request);
        AllRamMetricsResponse GetAllRamMetricsResponse(RamMetricCreateRequest request);
        AllDotNetMetricsResponse GetAllDotNetMetricsResponse(DotNetMetricCreateRequest request);
    }
}
