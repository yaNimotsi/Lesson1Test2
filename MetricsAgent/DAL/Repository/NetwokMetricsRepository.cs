using Dapper;

using MetricsAgent.DAL.Models;

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL.Repository
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetrics>
    {

    }
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private static readonly string ConnectionString = ConnectionStringToDataBase.ConnectionString;

        public void Create(NetworkMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("Insert into NetworkMetrics(value, time) Values(@value,@time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public List<NetworkMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<NetworkMetrics>("SELECT id, value, time FROM NetworkMetrics WHERE time >= @fromTime AND time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }
    }
}