using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Models;

namespace MetricsAgent
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetrics>
    {

    }
    public class NetworkMetricsRepository: INetworkMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public List<NetworkMetrics> GetByTimePeriod(DateTimeOffset startTimeSpan, DateTimeOffset endTimeSpan)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "SELECT id,value, time FROM NetworkMetrics WHERE time >= @startPeriod and time <= @endPeriod"
            };
            cmd.Parameters.AddWithValue("@startPeriod", startTimeSpan.ToUnixTimeMilliseconds());
            cmd.Parameters.AddWithValue("@endPeriod", endTimeSpan.ToUnixTimeMilliseconds());

            cmd.Prepare();

            var returnList = new List<NetworkMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetrics()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt64(2)
                    });
                }
            }
            return returnList;
        }

        public void Create(NetworkMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "Insert into NetworkMetrics(value, time) Values(@value,@time)"
            };
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}
