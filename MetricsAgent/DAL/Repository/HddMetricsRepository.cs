using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MetricsAgent.DAL.Models;

namespace MetricsAgent
{
    public interface IHddMetricsRepository : IRepository<HddMetrics>
    {
        List<HddMetrics> GetMaxDate();
    }
    public class HddMetricsRepository: IHddMetricsRepository
    {
        private static readonly string ConnectionString = ConnToDB.ConnectionString;

        public List<HddMetrics> GetByTimePeriod(DateTimeOffset startTimeSpan, DateTimeOffset endTimeSpan)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "SELECT id,value, time FROM HddMetrics WHERE time >= @startPeriod and time <= @endPeriod"
            };
            cmd.Parameters.AddWithValue("@startPeriod", startTimeSpan.ToUnixTimeMilliseconds());
            cmd.Parameters.AddWithValue("@endPeriod", endTimeSpan.ToUnixTimeMilliseconds());

            cmd.Prepare();

            var returnList = new List<HddMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetrics()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt64(2)
                    });
                }
            }
            return returnList;
        }

        public void Create(HddMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "Insert into HddMetrics(value, time) Values(@value,@time)"
            };
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
        public List<HddMetrics> GetMaxDate()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<HddMetrics>("SELECT id, value, time FROM HddMetrics WHERE time = (SELECT max(time) FROM CpuMetrics)").ToList();
            }
        }
    }
}
