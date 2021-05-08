using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repository
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;" +
                                                "Pooling=true;Max Pool Size=100";



        public IList<CpuMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics";

            var returnList = new List<CpuMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new CpuMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }

            return returnList;
        }

        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "Insert into cpumetrics(value, time) Values" +
                              "(@value,@time)"
            };
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}
