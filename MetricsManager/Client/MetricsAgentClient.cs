using MetricsManager.Client.Interface;
using MetricsManager.Client.Request;
using MetricsManager.Client.Response;

using Microsoft.Extensions.Logging;

using NLog;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(HttpClient client, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = client;
            _logger = logger;
        }

        public AllCpuMetricsApiResponse GetCpuMetricsFromAgent(AllCpuMetricsApiRequest request)
        {
            var fromTime = request.FromTime.ToString("s"); //2021-01-05T08:15:45
            var toTime = request.ToTime.ToString("s");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUri}/CpuAgent/byPeriod/fromTime/{fromTime}/toTime/{toTime}");

            try
            {
                var response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                
                var result = JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    },
                    CancellationToken.None).Result;

                return result.Metrics == null ? null : result;

                //return JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(0,e.Message);
            }

            return null;
        }

        public AllDotNetMetricsApiResponse GetDotNetMetricsFromAgent(AllDotNetCpuMetricsApiRequest request)
        {
            var fromTime = request.FromTime;
            var toTime = request.ToTime;

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUri}/DotNetAgent/byPeriod/from/{fromTime}/to/{toTime}");

            try
            {
                var response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    },
                    CancellationToken.None).Result; ;
            }
            catch (Exception e)
            {
                _logger.Log(0, e.Message);
            }

            return null;
        }

        public AllHddMetricsApiResponse GetHddMetricsFromAgent(AllHddMetricsApiRequest request)
        {
            var fromTime = request.FromTime;
            var toTime = request.ToTime;

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUri}/HddAgent/byPeriod/from/{fromTime}/to/{toTime}");

            try
            {
                var response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    },
                    CancellationToken.None).Result; ;
            }
            catch (Exception e)
            {
                _logger.Log(0, e.Message);
            }

            return null;
        }

        public AllNetworkMetricsApiResponse GetNetworkMetricsFromAgent(AllNetworkMetricsApiRequest request)
        {
            var fromTime = request.FromTime;
            var toTime = request.ToTime;

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUri}/NetworkAgent/byPeriod/from/{fromTime}/to/{toTime}");

            try
            {
                var response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllNetworkMetricsApiResponse>(responseStream, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    },
                    CancellationToken.None).Result; ;
            }
            catch (Exception e)
            {
                _logger.Log(0, e.Message);
            }

            return null;
        }

        public AllRamMetricsApiResponse GetRamMetricsFromAgent(AllRamMetricsApiRequest request)
        {
            var fromTime = request.FromTime;
            var toTime = request.ToTime;

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUri}/RamAgent/byPeriod/from/{fromTime}/to/{toTime}");

            try
            {
                var response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    },
                    CancellationToken.None).Result; ;
            }
            catch (Exception e)
            {
                _logger.Log(0, e.Message);
            }

            return null;
        }
    }
}
