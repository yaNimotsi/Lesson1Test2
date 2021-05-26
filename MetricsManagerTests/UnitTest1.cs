using System;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using ILogger = NLog.ILogger;

namespace MetricsManagerTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ILogger<CpuMetricsController>> _loggeMock;
        private ILogger<CpuMetricsController> _logger;
        private IMetricAgentClient _client;

        public CpuControllerUnitTests()
        {
            _loggeMock = new Mock<ILogger<CpuMetricsController>>();
            _controller = new CpuMetricsController(_loggeMock.Object, _client);
        }

        [Fact]
        public void GetMetricsFromAgent_RetunsOk()
        {
            var agentId = 1;

            //var fromTime = TimeSpan.FromSeconds(0);
            //var toTime = TimeSpan.FromSeconds(100);

            var fromTime = DateTimeOffset.FromUnixTimeMilliseconds(0);
            var toTime = DateTimeOffset.FromUnixTimeMilliseconds(100);

            //Act
            var result = _controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetMetricsFromAllClaster_RetunsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = _controller.GetMetricsFromAllCluster(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
    }

    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> loggerMock;
        public DotNetControllerUnitTests()
        {
            loggerMock = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(loggerMock.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_RetunsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetErrorsCount(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<ILogger<HddMetricsController>> loggerMock;
        
        public HddMetricsControllerUnitTests()
        {
            loggerMock = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(loggerMock.Object);
        }

        [Fact]
        public void GetFreeDiskSpace_RetunsOk()
        {
            //Act
            var result = controller.GetFreeDiskSpace();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetFreeDiskForPeriodOfTime_RetunsOk()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetFreeDiskForPeriodOfTime(agentId, fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class NetWorkMetricsControllerUnitTests
    {
        private NetworkMetricsController controller;
        private Mock<ILogger<NetworkMetricsController>> loggerMock;

        public NetWorkMetricsControllerUnitTests()
        {
            loggerMock = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(loggerMock.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_RetunsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetNetworkData(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> loggerMock;
        public RamMetricsControllerUnitTests()
        {
            loggerMock = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(loggerMock.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_RetunsOk()
        {
            //Act
            var result = controller.GetFreeSpaceRum();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetFreeRamForPeriodOfTime_RetunsOk()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetFreeRamForPeriodOfTime(agentId, fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
