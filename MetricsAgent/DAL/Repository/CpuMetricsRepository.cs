using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using MetricsAgent.DAL.Models;
using Dapper;

namespace MetricsAgent.DAL.Repository
{
    public interface ICpuMetricsRepository : IRepository<CpuMetrics>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
<<<<<<< HEAD
        private static readonly string ConnectionString = ConnToDB.ConnectionString;
        
=======
        private static readonly string ConnectionString = ConnectionStringToDataBase.ConnectionString;

>>>>>>> 5bcb79fa37e7434e1666d9d6b8ade533f11ea327
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
                return connection.Query<CpuMetrics>("SELECT id, value, time FROM CpuMetrics WHERE time >= @fromTime AND time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeMilliseconds(),
                        toTime = toTime.ToUnixTimeMilliseconds()
                    }).ToList();
            }
        }
    }
}