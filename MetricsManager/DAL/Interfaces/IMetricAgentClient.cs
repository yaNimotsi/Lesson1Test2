using MetricsAgent.DAL.Requests;
using MetricsManager.DAL.Models;
using MetricsManager.Request;
using MetricsManager.Response;

namespace MetricsManager.DAL.Interfaces
{
    public interface IMetricAgentClient
    {
        CpuMetrics GetMaxDateCpuMetricsInAgent(CpuMetricCreateRequest request);
        HddMetrics GetMaxDateHddMetricsInAgent(HddMetricCreateRequest request);
        NetworkMetrics GetMaxDateNetworkMetricsInAgent(NetworkMetricCreateRequest request);
        RamMetrics GetMaxDateRamMetricsInAgent(RamMetricCreateRequest request);
        DotNetMetrics GetMaxDateDotNetMetricsInAgent(DotNetMetricCreateRequest request);
        AllCpuMetricsResponse GetAllCpuMetricsResponse(CpuMetricCreateRequest request);
        AllHddMetricsResponse GetAllHddMetricsResponse(HddMetricCreateRequest request);
        AllNetworkMetricsResponse GetAllNetworkMetricsResponse(NetworkMetricCreateRequest request);
        AllRamMetricsResponse GetAllRamMetricsResponse(RamMetricCreateRequest request);
        AllDotNetMetricsResponse GetAllDotNetMetricsResponse(DotNetMetricCreateRequest request);
    }
}
