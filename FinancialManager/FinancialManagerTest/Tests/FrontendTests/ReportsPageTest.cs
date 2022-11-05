using Frontend.Pages.Reports;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Frontend.HttpService;
using FinancialManagerTest.Mocks.HttpServices;
using FinancialManagerTest.Extentions.BUnitExtentions;

namespace FinancialManagerTest.Tests.FrontendTests
{
    public class ReportsPageTest
    {
        [Fact]
        public void TestDailyReport()
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IHttpService, MockReportsHttpService>();
            var cut = context.RenderComponent<Reports>();
            cut.Instance.InputDailyReportDate = DateOnly.Parse("10.10.2002");
            var button = cut.GetElementBuyItsText("button", "Get daily report");
            button.Click();
            Assert.True(cut.HasMarkupElement("td", "Total income"));
            Assert.True(cut.HasMarkupElement("td", "10.00 UAH"));
            Assert.True(cut.HasMarkupElement("td", "Total exprenses"));
            Assert.True(cut.HasMarkupElement("td", "0.00 UAH"));
            Assert.True(cut.HasMarkupElement("td", "Got salary"));
            Assert.True(cut.HasMarkupElement("td", "10/10/2002 12:00:00 AM"));
        }

        [Fact]
        public void TestPeriodReport() 
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IHttpService, MockReportsHttpService>();
            var cut = context.RenderComponent<Reports>();
            cut.Instance.InputPeriodStart = DateOnly.Parse("10.10.2002");
            cut.Instance.InputPeriodEnd = DateOnly.Parse("10.12.2002");
            var button = cut.GetElementBuyItsText("button", "Get period report");
            button.Click();
            Assert.True(cut.HasMarkupElement("td", "Total income"));
            Assert.True(cut.HasMarkupElement("td", "20.00 UAH"));
            Assert.True(cut.HasMarkupElement("td", "Total exprenses"));
            Assert.True(cut.HasMarkupElement("td", "10.00 UAH"));
            Assert.True(cut.HasMarkupElement("td", "Got salary"));
            Assert.True(cut.HasMarkupElement("td", "Rent"));
            Assert.True(cut.HasMarkupElement("td", "11"));
            Assert.True(cut.HasMarkupElement("td", "12"));
            Assert.True(cut.HasMarkupElement("td", "10/10/2002 12:00:00 AM"));
            Assert.True(cut.HasMarkupElement("td", "10/11/2002 12:00:00 AM"));
            Assert.True(cut.HasMarkupElement("td", "10/12/2002 12:00:00 AM"));
        }
    }
}
