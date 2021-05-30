using MetricsManager.DAL.Client.Request;
using MetricsManager.DAL.Client.Response;

namespace MetricsManager.DAL.Client.Interface
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricsApiResponse GetCpuMetricsFromAgent(AllCpuMetricsApiRequest request);
        AllDotNetMetricsApiResponse GetDotNetMetricsFromAgent(AllDotNetCpuMetricsApiRequest request);
        AllHddMetricsApiResponse GetHddMetricsFromAgent(AllHddMetricsApiRequest request);
        AllNetworkMetricsApiResponse GetNetworkMetricsFromAgent(AllNetworkMetricsApiRequest request);
        AllRamMetricsApiResponse GetRamMetricsFromAgent(AllRamMetricsApiRequest request);
    }
}
