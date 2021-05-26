using MetricsAgent.DAL.Requests;

using MetricsManager.DAL.Interfaces;
using MetricsManager.Request;
using MetricsManager.Response;

using NLog;

using System;
using System.Net.Http;
using System.Text.Json;
using NLog.Fluent;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public MetricsAgentClient(HttpClient client, ILogger logger)
        {
            _httpClient = client;
            _logger = logger;
        }

        public AllCpuMetricsResponse GetAllCpuMetricsResponse(CpuMetricCreateRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeMilliseconds();
            var toTime = request.ToTime.ToUnixTimeMilliseconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentPath}/api/byPeriod/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllCpuMetricsResponse>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }

        public AllHddMetricsResponse GetAllHddMetricsResponse(HddMetricCreateRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeMilliseconds();
            var toTime = request.ToTime.ToUnixTimeMilliseconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentPath}/api/byPeriod/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllHddMetricsResponse>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }

        public AllNetworkMetricsResponse GetAllNetworkMetricsResponse(NetworkMetricCreateRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeMilliseconds();
            var toTime = request.ToTime.ToUnixTimeMilliseconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentPath}/api/byPeriod/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllNetworkMetricsResponse>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }

        public AllRamMetricsResponse GetAllRamMetricsResponse(RamMetricCreateRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeMilliseconds();
            var toTime = request.ToTime.ToUnixTimeMilliseconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentPath}/api/byPeriod/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllRamMetricsResponse>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }

        public AllDotNetMetricsResponse GetAllDotNetMetricsResponse(DotNetMetricCreateRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeMilliseconds();
            var toTime = request.ToTime.ToUnixTimeMilliseconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentPath}/api/byPeriod/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllDotNetMetricsResponse>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }
    }
}
