using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frontend.Pages.FinancialOperations;
using System.Threading.Tasks;
using Bunit;
using Frontend.HttpService;
using Microsoft.Extensions.DependencyInjection;
using FinancialManagerTest.Mocks.HttpServices;
using FinancialManagerTest.Mocks.Data;
using FinancialManagerTest.Extentions.BUnitExtentions;

namespace FinancialManagerTest.Tests.FrontendTests
{
    public class FinancialOperationsPagesTest
    {
        [Fact]
        public void FinancialOperationsIndexTest()
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IHttpService>(new MockFinancialOperationsHttpService());
            var cut = context.RenderComponent<Frontend.Pages.FinancialOperations.Index>();
            cut.WaitForAssertion(() => Assert.Equal(20, cut.FindAll("td").Count));
        }

        [Fact]
        public void FinancialOperationsDetailsTest()
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IHttpService>(new MockFinancialOperationsHttpService());
            var cut = context.RenderComponent<Details>(paremeters => paremeters.Add(p => p.Id, 123));
            Assert.True(cut.HasMarkupElement("td", "123"));
            Assert.True(cut.HasMarkupElement("td", "10.00 UAH"));
            Assert.True(cut.HasMarkupElement("td", "11"));
            Assert.True(cut.HasMarkupElement("td", "10.10.2002 00:00:00"));
        }

        [Fact]
        public void FinancialOperationsDeleteTest()
        {
            var dbContext = new MockFinancialManagerContext();
            var context = new TestContext();
            context.Services.AddSingleton<IHttpService>(new MockFinancialOperationsHttpService(dbContext));
            var cut = context.RenderComponent<Delete>(parameters => parameters.Add(p => p.Id, 123));
            var buttons = cut.FindAll("button");
            var requiredButton = buttons.FirstOrDefault(element => element.HasMarkupElement("Delete"));
            Assert.NotNull(requiredButton);
            requiredButton.Click();
            Assert.Equal(3, dbContext.FinancialOperations.Count());
        }
    }
}
