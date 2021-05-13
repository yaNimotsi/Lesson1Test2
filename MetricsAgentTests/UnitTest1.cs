using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using MetricsAgent;
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
        private readonly CpuAgentController _controller;
        private readonly Mock<ICpuMetricsRepository> _mock;

        public CpuMetricsControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            var mockLogger = new Mock<ILogger<CpuAgentController>>();

            _controller = new CpuAgentController(mockLogger.Object, _mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект

            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetrics>()));

            // выполняем действие на контроллере
            //var result = _controller.Create(new DotNetMetricCreateRequest() { Time = 12, Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<CpuMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // выполняем действие на контроллере
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }

    public class DotNetMetricsControllerUnitTests
    {
        private readonly DotNetAgentController _controller;
        private readonly Mock<IDotNetMetricsRepository> _mock;

        public DotNetMetricsControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            var mockLogger = new Mock<ILogger<DotNetAgentController>>();

            _controller = new DotNetAgentController(mockLogger.Object, _mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект

            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetrics>()));

            // выполняем действие на контроллере
            //var result = _controller.Create(new DotNetMetricCreateRequest() { Time = 12, Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<DotNetMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // выполняем действие на контроллере
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }

    public class HddMetricsControllerUnitTests
    {
        private readonly HddAgentController _controller;
        private readonly Mock<IHddMetricsRepository> _mock;

        public HddMetricsControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();
            var mockLogger = new Mock<ILogger<HddAgentController>>();

            _controller = new HddAgentController(mockLogger.Object, _mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект

            _mock.Setup(repository => repository.Create(It.IsAny<HddMetrics>()));

            // выполняем действие на контроллере
            //var result = _controller.Create(new HddMetricCreateRequest() { Time = 12, Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<HddMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<HddMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // выполняем действие на контроллере
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }

    public class NetworkMetricsControllerUnitTests
    {
        private readonly NetworkAgentController _controller;
        private readonly Mock<INetworkMetricsRepository> _mock;

        public NetworkMetricsControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            var mockLogger = new Mock<ILogger<NetworkAgentController>>();

            _controller = new NetworkAgentController(mockLogger.Object, _mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект

            _mock.Setup(repository => repository.Create(It.IsAny<NetworkMetrics>()));

            // выполняем действие на контроллере
            //var result = _controller.Create(new NetworkMetricCreateRequest() { Time = 12, Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<NetworkMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<NetworkMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // выполняем действие на контроллере
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }

    public class RamMetricsControllerUnitTests
    {
        private readonly RamAgentController _controller;
        private readonly Mock<IRamMetricsRepository> _mock;

        public RamMetricsControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            var mockLogger = new Mock<ILogger<RamAgentController>>();

            _controller = new RamAgentController(mockLogger.Object, _mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект

            _mock.Setup(repository => repository.Create(It.IsAny<RamMetrics>()));

            // выполняем действие на контроллере
            //var result = _controller.Create(new RamMetricCreateRequest() { Time = 12, Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<RamMetrics>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<RamMetrics>());

            var dateTimeOffset1 = DateTimeOffset.Now;
            var dateTimeOffset2 = DateTimeOffset.Now;
            // выполняем действие на контроллере
            var result = _controller.GetByTimePeriod(dateTimeOffset1, dateTimeOffset2);

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
