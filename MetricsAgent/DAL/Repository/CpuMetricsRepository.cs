using Dapper;

using MetricsAgent.DAL.Models;

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL.Repository
{
    public interface ICpuMetricsRepository : IRepository<CpuMetrics>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;
        
        public void Create(CpuMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into CpuMetrics(value, time) Values(@value,@time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public List<CpuMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var fromUnix = fromTime.ToUnixTimeSeconds();
                var toUnix = toTime.ToUnixTimeSeconds();

                var test = connection.Query<CpuMetrics>("SELECT id, value, time FROM CpuMetrics WHERE time >= @fromTime AND time <= @toTime",
                    new
                    {
                        fromTime = fromUnix,
                        toTime = toUnix
                    }).ToList();
                return connection.Query<CpuMetrics>("SELECT id, value, time FROM CpuMetrics WHERE time >= @fromTime AND time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }
    }
}