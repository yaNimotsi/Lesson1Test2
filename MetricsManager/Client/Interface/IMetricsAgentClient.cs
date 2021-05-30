using MetricsManager.Client.Request;
using MetricsManager.Client.Response;

namespace MetricsManager.Client.Interface
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
