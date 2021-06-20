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
            //Отлов всех исключений в рамках работы приложения
            catch (Exception e)
            {
                //Nlog: устанавливаем отлов приложений
                logger.Error(e, "Stoped program because of extension");
                throw;
            }
            finally
            {
                //Отсановка логера
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
                    //Создание провайдеров логирования
                    logging.ClearProviders();
                    //устанавливаем минимальный уровень логирования
                    logging.SetMinimumLevel(LogLevel.Trace);
                    //Добавляем библиотеку nLog
                }).UseNLog();
    }
}
