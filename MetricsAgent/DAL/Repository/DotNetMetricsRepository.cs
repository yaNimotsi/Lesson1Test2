using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repository
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetrics>
    {

    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;

        public List<DotNetMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetrics>("SELECT id,value, time FROM DotNetMetrics WHERE time >= @startPeriod and time <= @endPeriod",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }

        public void Create(DotNetMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into DotNetMetrics(value, time) Values(@value,@time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }
    }
}
