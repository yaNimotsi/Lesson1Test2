using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public static class ConnToDB
    {
        private const string connToDB = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100";
        public static string ConnectionString => connToDB;
    }
}
