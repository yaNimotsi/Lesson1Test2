using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Models;

namespace MetricsAgent
{
    public interface IRamMetricsRepository : IRepository<RamMetrics>
    {

    }
    public class RamMetricsRepository: IRamMetricsRepository
    {
        private static readonly string ConnectionString = ConnectionStringToDataBase.ConnectionString;

        public List<RamMetrics> GetByTimePeriod(DateTimeOffset startTimeSpan, DateTimeOffset endTimeSpan)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "SELECT id,value, time FROM RamMetrics WHERE time >= @startPeriod and time <= @endPeriod"
            };
            cmd.Parameters.AddWithValue("@startPeriod", startTimeSpan.ToUnixTimeMilliseconds());
            cmd.Parameters.AddWithValue("@endPeriod", endTimeSpan.ToUnixTimeMilliseconds());

            cmd.Prepare();

            var returnList = new List<RamMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new RamMetrics()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt64(2)
                    });
                }
            }
            return returnList;
        }

        public void Create(RamMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "Insert into RamMetrics(value, time) Values(@value,@time)"
            };
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}
