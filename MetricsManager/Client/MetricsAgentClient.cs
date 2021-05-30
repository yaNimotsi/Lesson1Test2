using System;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Client.Interface;
using MetricsManager.Client.Request;
using MetricsManager.Client.Response;
using NLog;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public MetricsAgentClient(HttpClient client, ILogger logger)
        {
            _httpClient = client;
            _logger = logger;
        }

        public AllCpuMetricsApiResponse GetCpuMetricsFromAgent(AllCpuMetricsApiRequest request)
        {
            var fromTime = request.FromTime;
            var toTime = request.ToTime;

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUri}/api/cpumetrics/from/{fromTime}/to/{toTime}");

            try
            {
                var response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }

            return null;
        }

        public AllDotNetMetricsApiResponse GetDotNetMetricsFromAgent(AllDotNetCpuMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllHddMetricsApiResponse GetHddMetricsFromAgent(AllHddMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllNetworkMetricsApiResponse GetNetworkMetricsFromAgent(AllNetworkMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllRamMetricsApiResponse GetRamMetricsFromAgent(AllRamMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
