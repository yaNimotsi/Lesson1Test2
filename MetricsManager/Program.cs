using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace MetricsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            //����� ���� ���������� � ������ ������ ����������
            catch (Exception e)
            {
                //Nlog: ������������� ����� ����������
                logger.Error(e, "Stoped program because of extension");
                throw;
            }
            finally
            {
                //��������� ������
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    //�������� ����������� �����������
                    logging.ClearProviders();
                    //������������� ����������� ������� �����������
                    logging.SetMinimumLevel(LogLevel.Trace);
                    //��������� ���������� nLog
                }).UseNLog();
    }
}
