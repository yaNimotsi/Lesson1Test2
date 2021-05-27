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
        List<DotNetMetrics> GetMaxDate();
    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;

        public List<DotNetMetrics> GetByTimePeriod(DateTimeOffset startTimeSpan, DateTimeOffset endTimeSpan)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "SELECT id,value, time FROM DotNetmetrics WHERE time >= @startPeriod and time <= @endPeriod"
            };
            cmd.Parameters.AddWithValue("@startPeriod", startTimeSpan.ToUnixTimeMilliseconds());
            cmd.Parameters.AddWithValue("@endPeriod", endTimeSpan.ToUnixTimeMilliseconds());

            cmd.Prepare();

            var returnList = new List<DotNetMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetrics()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt64(2)
                    });
                }
            }
            return returnList;
        }

        public void Create(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "Insert into DotNetmetrics(value, time) Values(@value,@time)"
            };
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public List<DotNetMetrics> GetMaxDate()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetrics>("SELECT id, value, time FROM DotNetmetrics WHERE time = (SELECT max(time) FROM CpuMetrics)").ToList();
            }
        }
    }
}
