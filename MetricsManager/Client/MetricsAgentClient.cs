using MetricsAgent.DAL.Requests;

using MetricsManager.DAL.Interfaces;
using MetricsManager.Request;
using MetricsManager.Response;

using NLog;

using System;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.DAL.Models;
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

        public CpuMetrics GetMaxDateCpuMetricsInAgent(CpuMetricCreateRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUri}/api/MaxDate");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<CpuMetrics>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }

        public HddMetrics GetMaxDateHddMetricsInAgent(HddMetricCreateRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUri}/api/MaxDate");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<HddMetrics>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }

        public NetworkMetrics GetMaxDateNetworkMetricsInAgent(NetworkMetricCreateRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUri}/api/MaxDate");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<NetworkMetrics>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }

        public RamMetrics GetMaxDateRamMetricsInAgent(RamMetricCreateRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUri}/api/MaxDate");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<RamMetrics>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }

        public DotNetMetrics GetMaxDateDotNetMetricsInAgent(DotNetMetricCreateRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUri}/api/MaxDate");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<DotNetMetrics>(responseStream).Result;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }

        public AllCpuMetricsResponse GetAllCpuMetricsResponse(CpuMetricCreateRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeMilliseconds();
            var toTime = request.ToTime.ToUnixTimeMilliseconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUri}/api/byPeriod/from/{fromTime}/to/{toTime}");

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
                $"{request.AgentUri}/api/byPeriod/from/{fromTime}/to/{toTime}");

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
                $"{request.AgentUri}/api/byPeriod/from/{fromTime}/to/{toTime}");

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
                $"{request.AgentUri}/api/byPeriod/from/{fromTime}/to/{toTime}");

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
                $"{request.AgentUri}/api/byPeriod/from/{fromTime}/to/{toTime}");

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
