using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repository
{
    public interface IRamMetricsRepository : IRepository<RamMetrics>
    {

    }
    public class RamMetricsRepository: IRamMetricsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;

        public List<RamMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetrics>("SELECT id,value, time FROM RamMetrics WHERE time >= @startPeriod and time <= @endPeriod",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public void Create(RamMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into RamMetrics(value, time) Values(@value,@time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }
    }
}
