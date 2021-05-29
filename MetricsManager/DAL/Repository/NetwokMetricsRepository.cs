using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repository
{
    public interface INetworkMetricsRepository : IMetricsRepository<NetworkMetrics>
    {

    }
    public class NetworkMetricsRepository: INetworkMetricsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;

        public List<NetworkMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<NetworkMetrics>("SELECT id, agentId, value, time FROM NetworkMetrics WHERE time >= @startPeriod and time <= @endPeriod",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public List<NetworkMetrics> GetByAgentAndTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<NetworkMetrics>("SELECT id, agentId, value, time FROM NetworkMetrics WHERE agentId = @agentId and time >= @fromTime AND time <= @toTime",
                    new
                    {
                        agentId = agentId,
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public void Create(NetworkMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into NetworkMetrics(agentId, value, time) Values(@agentId,@value,@time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }
    }
}
