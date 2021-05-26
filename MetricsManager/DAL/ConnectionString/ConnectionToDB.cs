namespace MetricsManager.DAL.ConnectionString
{
    public class ConnectionToDB
    {
        private const string connToDB = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100";
        public static string ConnectionString => connToDB;
    }
}
