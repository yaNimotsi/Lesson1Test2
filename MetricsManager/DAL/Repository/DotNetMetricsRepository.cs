using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repository
{
    public interface IDotNetMetricsRepository : IMetricsRepository<DotNetMetrics>
    {

    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;

        public List<DotNetMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetrics>("SELECT Id,Value, Time FROM DotNetMetrics WHERE Time >= @startPeriod and Time <= @endPeriod",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public List<DotNetMetrics> GetByAgentAndTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetrics>("SELECT Id, agentId, Value, Time FROM DotNetMetrics WHERE agentId = @agentId and Time >= @fromTime AND Time <= @toTime",
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
                connection.Execute("SELECT max(Time) from DotNetMetrics where agentId = @agentId",
                    new
                    {
                        agentId = agentId
                    });
            }

            return DateTimeOffset.UtcNow;
        }

        public void Create(DotNetMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into DotNetMetrics(Value, Time) Values(@Value,@Time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }
    }
}
