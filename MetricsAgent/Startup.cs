using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SQLite;
using MetricsAgent.DAL.Repository;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);

            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                //remove old table if exists
                command.CommandText = "DROP TABLE IF EXISTS CpuMetrics";
                command.ExecuteNonQuery();

                //create new table
                command.CommandText = @"CREATE TABLE CpuMetrics(id INTEGER Not Null PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery(); 

                //Add some fake data in db
                command.CommandText = "INSERT INTO CpuMetrics VALUES(1,10,1617223300000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO CpuMetrics VALUES(2,20,1617223500000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO CpuMetrics VALUES(3,30,1617225000000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO CpuMetrics VALUES(4,40,1617664400000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO CpuMetrics VALUES(5,50,1620766900000)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS CpuMetrics";
                command.ExecuteNonQuery();

                //create new table
                command.CommandText = @"CREATE TABLE DotNetMetrics(id INTEGER Not Null PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();

                //Add some fake data in db
                command.CommandText = "INSERT INTO DotNetMetrics VALUES(1,10,1617223300000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO DotNetMetrics VALUES(2,20,1617223500000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO DotNetMetrics VALUES(3,30,1617225000000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO DotNetMetrics VALUES(4,40,1617664400000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO DotNetMetrics VALUES(5,50,1620766900000)";
                command.ExecuteNonQuery();

                //create new table
                command.CommandText = @"CREATE TABLE HddMetrics(id INTEGER Not Null PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();

                //Add some fake data in db
                command.CommandText = "INSERT INTO HddMetrics VALUES(1,10,1617223300000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO HddMetrics VALUES(2,20,1617223500000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO HddMetrics VALUES(3,30,1617225000000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO HddMetrics VALUES(4,40,1617664400000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO HddMetrics VALUES(5,50,1620766900000)";
                command.ExecuteNonQuery();

                //create new table
                command.CommandText = @"CREATE TABLE NetworkMetrics(id INTEGER Not Null PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();

                //Add some fake data in db
                command.CommandText = "INSERT INTO NetworkMetrics VALUES(1,10,1617223300000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO NetworkMetrics VALUES(2,20,1617223500000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO NetworkMetrics VALUES(3,30,1617225000000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO NetworkMetrics VALUES(4,40,1617664400000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO NetworkMetrics VALUES(5,50,1620766900000)";
                command.ExecuteNonQuery();

                //create new table
                command.CommandText = @"CREATE TABLE RamMetrics(id INTEGER Not Null PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();

                //Add some fake data in db
                command.CommandText = "INSERT INTO RamMetrics VALUES(1,10,1617223300000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO RamMetrics VALUES(2,20,1617223500000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO RamMetrics VALUES(3,30,1617225000000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO RamMetrics VALUES(4,40,1617664400000)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO RamMetrics VALUES(5,50,1620766900000)";
                command.ExecuteNonQuery();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
