using System;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using ILogger = NLog.ILogger;

namespace MetricsManagerTests
{
    public class CpuControllerUnitTests
    {
        /*private CpuMetricsController controller;
        private ILogger<CpuMetricsController> logger;

        public CpuControllerUnitTests()
        {
            controller = new CpuMetricsController(ILogger < CpuMetricsController > logger);
        }

        [Fact]
        public void GetMetricsFromAgent_RetunsOk()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetMetricsFromAllClaster_RetunsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        */
    }

    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;

        public DotNetControllerUnitTests()
        {
            controller = new DotNetMetricsController();
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

        public HddMetricsControllerUnitTests()
        {
            controller = new HddMetricsController();
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

        public NetWorkMetricsControllerUnitTests()
        {
            controller = new NetworkMetricsController();
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

        public RamMetricsControllerUnitTests()
        {
            controller = new RamMetricsController();
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
