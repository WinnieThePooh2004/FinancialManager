using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManager.Services.ReportService;
using FinancialManagerTest.Mocks;
using FinancialManagerTest.Mocks.Data;
using FinancialManager.MapperProfiles;

namespace FinancialManagerTest.Tests.BackendTests
{
    public class TestReportService
    {
        [Fact]
        public async Task TestDailyReport()
        {
            var service = CreateService();
            var report = await service.DailyReportAsync(DateTime.Parse("10.10.2002"));
            Assert.NotNull(report);
            Assert.Equal(1000, report.TotalIncome);
            Assert.Equal(0, report.TotalExprenses);
            Assert.Single(report.Operations);
        }

        [Fact]
        public async Task TestPeriodReport()
        {
            var service = CreateService();
            var report = await service.PeriodReportAsync(DateTime.Parse("09.11.2002"), DateTime.Parse("10.12.2002"));
            Assert.NotNull(report);
            Assert.Equal(1000, report.TotalExprenses);
            Assert.Equal(1000, report.TotalIncome);
            Assert.Equal(2, report.Operations.Count);
        }

        [Fact]
        public async Task TestEmptyDailyReport()
        {
            var service = CreateService();
            var report = await service.DailyReportAsync(DateTime.Parse("10.10.2010"));
            Assert.NotNull(report);
            Assert.Equal(0, report.TotalIncome);
            Assert.Equal(0, report.TotalExprenses);
            Assert.Empty(report.Operations);
        }

        [Fact]
        public async Task TestEmptyPeriodReport()
        {
            var service = CreateService();
            var report = await service.PeriodReportAsync(DateTime.Parse("09.10.2009"), DateTime.Parse("10.10.2010"));
            Assert.NotNull(report);
            Assert.Equal(0, report.TotalIncome);
            Assert.Equal(0, report.TotalExprenses);
            Assert.Empty(report.Operations);
        }

        private ReportService CreateService()
        {
            return new ReportService(new MockFinancialManagerContext());
        }
    }
}
