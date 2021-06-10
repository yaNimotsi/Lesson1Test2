using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repository
{
    public interface ICpuMetricsRepository : IMetricsRepository<CpuMetrics>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;

        public DateTimeOffset GetMaxDate(long agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var result = connection.Query<long>("SELECT max(Time) from CpuMetrics where agentId = @agentId", 
                    new
                    {
                        agentId = agentId
                    }).ToList();

                if (result != null)
                    return DateTimeOffset.FromUnixTimeMilliseconds(result[0]);
            }

            return DateTimeOffset.UtcNow;
        }

        public void Create(CpuMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into CpuMetrics(agentId, Value, Time) Values(@agentId,@Value,@Time)",
                    new
                    {
                        agentId = item.AgentId,
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public List<CpuMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<CpuMetrics>("SELECT Id, agentId, Value, Time FROM CpuMetrics WHERE Time >= @fromTime AND Time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public List<CpuMetrics> GetByAgentAndTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<CpuMetrics>("SELECT Id, agentId, Value, Time FROM CpuMetrics WHERE agentId = @agentId and Time >= @fromTime AND Time <= @toTime",
                    new
                    {
                        agentId = agentId,
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }
    }
}