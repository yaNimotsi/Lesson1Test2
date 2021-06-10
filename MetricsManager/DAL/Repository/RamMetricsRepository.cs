using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repository
{
    public interface IRamMetricsRepository : IMetricsRepository<RamMetrics>
    {

    }
    public class RamMetricsRepository: IRamMetricsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;

        public List<RamMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetrics>("SELECT Id, agentId, Value, Time FROM RamMetrics WHERE Time >= @startPeriod and Time <= @endPeriod",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public List<RamMetrics> GetByAgentAndTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetrics>("SELECT Id, agentId, Value, Time FROM RamMetrics WHERE agentId = @agentId and Time >= @fromTime AND Time <= @toTime",
                    new
                    {
                        agentId = agentId,
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public DateTimeOffset GetMaxDate(long agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("SELECT max(Time) from RamMetrics where agentId = @agentId",
                    new
                    {
                        agentId = agentId
                    });
            }

            return DateTimeOffset.UtcNow;
        }

        public void Create(RamMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into RamMetrics(agentId, Value, Time) Values(@agentId,@Value,@Time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }
    }
}
