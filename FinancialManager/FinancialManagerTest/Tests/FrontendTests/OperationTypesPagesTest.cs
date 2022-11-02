using Bunit;
using FinancialManagerTest.Mocks.Data;
using FinancialManagerTest.Mocks.HttpServices;
using Frontend.HttpService;
using Microsoft.Extensions.DependencyInjection;
using Frontend.Pages.OperationTypes;
using FinancialManagerTest.Extentions.BUnitExtentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOs.OperationTypes;

namespace FinancialManagerTest.Tests.FrontendTests
{
    public class OperationTypesPagesTest
    {
        [Fact]
        public void OperationTypesIndexTest()
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IHttpService, MockOperationTypesHttpService>();
            var cut = context.RenderComponent<Frontend.Pages.OperationTypes.Index>();
            cut.WaitForAssertion(() => Assert.Equal(8, cut.FindAll("td").Count));
        }

        [Fact]
        public void OperationTypesDetailsTest()
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IHttpService, MockOperationTypesHttpService>();
            var cut = context.RenderComponent<Details>(paremeters => paremeters.Add(p => p.Id, 11));
            cut.WaitForAssertion(() => Assert.Equal(6, cut.FindAll("td").Count));
            Assert.True(cut.HasMarkupElement("td", "11"));
            Assert.True(cut.HasMarkupElement("td", "True"));
            Assert.True(cut.HasMarkupElement("td", "salary"));
        }

        [Fact]
        public void OperationTypesDeleteTest()
        {
            var dbContext = new MockFinancialManagerContext();
            using var context = new TestContext();
            context.Services.AddSingleton<IHttpService>(new MockOperationTypesHttpService(dbContext));
            var cut = context.RenderComponent<Delete>(paremeters => paremeters.Add(p => p.Id, 11));
            var buttons = cut.FindAll("button");
            var requiredButton = buttons.FirstOrDefault(element => element.HasMarkupElement("Delete"));
            Assert.NotNull(requiredButton);
            requiredButton.Click();
            Assert.Equal(1, dbContext.OperationTypes.Count());
            Assert.Equal(2, dbContext.FinancialOperations.Count());
        }

        [Fact]
        public void OperationTypesCreateTest()
        {
            var dbContext = new MockFinancialManagerContext();
            using var context = new TestContext();
            context.Services.AddSingleton<IHttpService>(new MockOperationTypesHttpService(dbContext));
            var cut = context.RenderComponent<Create>();
            cut.Instance.CreateDto = new OperationTypeCreateDto
            {
                IsIncome = true,
                Name = "Test"
            };
            var buttons = cut.FindAll("button");
            var requiredButton = buttons.FirstOrDefault(element => element.HasMarkupElement("Save"));
            Assert.NotNull(requiredButton);
            requiredButton.Click();
            Assert.Equal(3, dbContext.OperationTypes.Count());
            Assert.NotNull(dbContext.OperationTypes.FirstOrDefault(type => type.IsIncome && type.Name == "Test"));
        }

        [Fact]
        public void OperationTypesUpdateTest()
        {
            var dbContext = new MockFinancialManagerContext();
            using var context = new TestContext();
            context.Services.AddSingleton<IHttpService>(new MockOperationTypesHttpService(dbContext));
            var cut = context.RenderComponent<Update>(parameters => parameters.Add(p => p.Id, 11));
            cut.Instance.UpdateDto = new OperationTypeUpdateDto
            {
                Id = 11,
                IsIncome = false,
                Name = "Test"
            };
            var buttons = cut.FindAll("button");
            var requiredButton = buttons.FirstOrDefault(element => element.HasMarkupElement("Save"));
            Assert.NotNull(requiredButton);
            requiredButton.Click();
            Assert.NotNull(dbContext.OperationTypes.FirstOrDefault(type => !type.IsIncome && type.Name == "Test"));
            Assert.Equal(2, dbContext.OperationTypes.Count());
            Assert.NotNull(dbContext.OperationTypes.FirstOrDefault(type => !type.IsIncome && type.Name == "rent"));
        }
    }
}
