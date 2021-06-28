using System;
using AutoMapper;

using FluentMigrator.Runner;

using MetricsAgent.DAL.Jobs;
using MetricsAgent.DAL.Jobs.Jobs;
using MetricsAgent.DAL.Jobs.Schedule;
using MetricsAgent.DAL.Repository;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Quartz;
using Quartz.Impl;
using Quartz.Spi;

using System.Data.SQLite;
using System.IO;
using System.Reflection;
using MetricsAgent.DAL.Jobs.HostedService;
using Microsoft.OpenApi.Models;


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

            var mapperConfiguration = new MapperConfiguration(mp =>
                mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(ConnToDB.ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType:typeof(CpuMetricJob),
                cronExpression: "0/5 * * * * ?"));

            /*services.AddSingleton<DotNetMetricsJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotNetMetricsJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<HddMetricsJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricsJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<NetworkMetricsJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricsJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<RamMetricsJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricsJob),
                cronExpression: "0/5 * * * * ?"));*/

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API сервиса агента сбора метрик",
                    Description = "Тут можно поиграть с api нашего сервиса",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Nimot",
                        Email = string.Empty,
                        //Url = new Uri("https://kremlin.ru"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "можно указать под какой лицензией все опубликовано",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            var connection = new SQLiteConnection(ConnToDB.ConnectionString);
            connection.Open();
        }

        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
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


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API сервиса сбора метрик");
                //С нулевым префиксом не заводится
                //c.RoutePrefix = string.Empty;
            });

            migrationRunner.MigrateUp();
        }
    }
}
