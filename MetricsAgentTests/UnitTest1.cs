using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using MetricsAgent.DAL.Requests;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NLog;
using Xunit;
using ILogger = NLog.ILogger;

namespace MetricsAgentTests
{ 
    public class CpuMetricsControllerUnitTests
    {
        private readonly CpuAgentController controller;
        private readonly Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CpuAgentController>> _mockLogger;

        public CpuMetricsControllerUnitTests()
        {
            //var logger = new Mock<ILogger>();
            mock = new Mock<ICpuMetricsRepository>();
            //var logger = new Mock<ILogger<CpuAgentController>>();
            var _mockLogger = new Mock<ILogger<CpuAgentController>>();

            controller = new CpuAgentController(_mockLogger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>()));
            //mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new CpuMetricCreateRequest() { Time = 12, Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetAll_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект

            mock.Setup(repository => repository.GetAll());
            

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

    }
}
