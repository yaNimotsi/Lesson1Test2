using System;
using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.Client;
using MetricsManager.DAL.ConnectionString;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Jobs;
using MetricsManager.DAL.Jobs.HostedService;
using MetricsManager.DAL.Jobs.Jobs;
using MetricsManager.DAL.Jobs.Schedule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsManager
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
            services.AddSingleton<AgentInfo>();


            var mapperConfiguration = new MapperConfiguration(mp =>
                mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);


            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(ConnectionToDB.ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddHttpClient<IMetricAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(CpuMetricJob),
                "0/5 * * * * ?"));
            
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(CpuMetricJob),
                "0/5 * * * * ?"));
            
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(CpuMetricJob),
                "0/5 * * * * ?"));
            
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(CpuMetricJob),
                "0/5 * * * * ?"));
            
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(CpuMetricJob),
                "0/5 * * * * ?"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
