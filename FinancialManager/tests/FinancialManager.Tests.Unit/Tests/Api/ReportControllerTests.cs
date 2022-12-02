using FinancialManager.Shared.Interfaces.Services;
using FinancialManager.Controllers;
using FinancialManager.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.Tests.Unit.Tests.Api
{
    public class ReportControllerTests
    {
        private readonly ReportsController _controller;
        private readonly IReportService _service = Substitute.For<IReportService>();

        public ReportControllerTests()
        {
            _controller = new ReportsController(_service);
        }

        [Fact]
        public async void GetDailyReport_ReturnsFromService_ReturnsOkResult()
        {
            _service.DailyReportAsync(Arg.Any<DateTime>()).Returns(Task.FromResult(_report));
            var actual = await _controller.GetDailyReport(DateTime.Now);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(_report);
        }

        [Fact]
        public async void GetPeriodReport_ReturnsFromService_ReturnsOkResult()
        {
            _service.PeriodReportAsync(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(Task.FromResult(_report));
            var actual = await _controller.GetReportByPeriod(DateTime.Now, DateTime.Now);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(_report);
        }

        private readonly ReportDTO _report = new()
        {
            TotalExprenses = 100,
            TotalIncome = 100,
            Operations = new()
            {
                new FinancialOperationDTO{ Amount = 100, DateTime = DateTime.Now, Description = "income" },
                new FinancialOperationDTO{ Amount = 100, DateTime = DateTime.Now, Description = "exprense" },
            }
        };
    }
}