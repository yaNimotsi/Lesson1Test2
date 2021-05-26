using Dapper;

using MetricsManager.DAL.ConnectionString;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent
{
    public interface INetworkMetricsManagerRepository : IRepository<NetworkMetrics>
    {

    }
    public class NetworkMetricsRepository: INetworkMetricsManagerRepository
    {
        

        private static readonly string ConnectionString = ConnectionToDB.ConnectionString;

        public void Create(NetworkMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into NetworkMetrics(agentId, value, time) Values(@agentId,@value,@time)",
                    new
                    {
                        agentid = item.AgentId,
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public List<NetworkMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<NetworkMetrics>("SELECT id, agentId, value, time FROM NetworkMetrics WHERE time >= @fromTime AND time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public List<NetworkMetrics> GetByTimePeriodFromAgent(int agentId, DateTimeOffset item, DateTimeOffset item2)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<NetworkMetrics>("SELECT id, agentId, value, time FROM NetworkMetrics WHERE time >= @fromTime AND time <= @toTime AND agentId = @thisAgentId",
                    new
                    {
                        thisAgentId = agentId,
                        fromTime = item.ToUnixTimeMilliseconds(),
                        toTime = item2.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }
    }
}
