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
    public interface IHddMetricsManagerRepository : IRepository<HddMetrics>
    {

    }
    public class HddMetricsRepository: IHddMetricsManagerRepository
    {

        private static readonly string ConnectionString = ConnectionToDB.ConnectionString;

        public void Create(HddMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into HddMetrics(agentId, value, time) Values(@agentId,@value,@time)",
                    new
                    {
                        agentid = item.AgentId,
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public List<HddMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<HddMetrics>("SELECT id, agentId, value, time FROM HddMetrics WHERE time >= @fromTime AND time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public List<HddMetrics> GetByTimePeriodFromAgent(int agentId, DateTimeOffset item, DateTimeOffset item2)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<HddMetrics>("SELECT id, agentId, value, time FROM HddMetrics WHERE time >= @fromTime AND time <= @toTime AND agentId = @thisAgentId",
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
