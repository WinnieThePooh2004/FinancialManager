using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManager.Services.ReportService;
using FinancialManagerTest.Mocks;
using FinancialManagerTest.Mocks.Data;
using FinancialManager.MapperProfiles;
using FinancialManager.Controllers;
using AutoMapper;
using FinancialManager.MapperProfiles.ReportProfiles;

namespace FinancialManagerTest.Tests.BackendTests
{
    public class TestReportService
    {
        [Fact]
        public async Task TestDailyReport()
        {
            var controller = CreateController();
            var response = await controller.GetDailyReport(DateTime.Parse("10.10.2002"));
            var report = response.Value;
            Assert.NotNull(report);
            Assert.Equal("10.00 UAH", report.TotalIncome);
            Assert.Equal("0.00 UAH", report.TotalExprenses);
            Assert.Single(report.Operations);
        }

        [Fact]
        public async Task TestPeriodReport()
        {
            var controller = CreateController();
            var response = await controller.GetReportByPeriod(DateTime.Parse("09.11.2002"), DateTime.Parse("10.12.2002"));
            var report = response.Value;
            Assert.NotNull(report);
            Assert.Equal("10.00 UAH", report.TotalExprenses);
            Assert.Equal("10.00 UAH", report.TotalIncome);
            Assert.Equal(2, report.Operations.Count);
        }

        [Fact]
        public async Task TestEmptyDailyReport()
        {
            var controller = CreateController();
            var response = await controller.GetDailyReport(DateTime.Parse("10.10.2010"));
            var report = response.Value;
            Assert.NotNull(report);
            Assert.Equal("0.00 UAH", report.TotalIncome);
            Assert.Equal("0.00 UAH", report.TotalExprenses);
            Assert.Empty(report.Operations);
        }

        [Fact]
        public async Task TestEmptyPeriodReport()
        {
            var service = CreateController();
            var response = await service.GetReportByPeriod(DateTime.Parse("09.10.2009"), DateTime.Parse("10.10.2010"));
            var report = response.Value;
            Assert.NotNull(report);
            Assert.Equal("0.00 UAH", report.TotalIncome);
            Assert.Equal("0.00 UAH", report.TotalExprenses);
            Assert.Empty(report.Operations);
        }

        private ReportsController CreateController()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ReportDatailsProfile()));
            return new ReportsController(new ReportService(new MockFinancialManagerContext()), new Mapper(config));
        }
    }
}
