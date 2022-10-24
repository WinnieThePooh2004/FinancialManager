using FinancialManager.Controllers;
using FinancialManager.DTOs.FinancialOperations;
using FinancialManager.Models;
using AutoMapper;
using FinancialManager.MapperProfiles.FinancialOperations;
using Microsoft.AspNetCore.Mvc;
using FinancialManager.Services.CRUDServices;
using FinancialManagerTest.Mocks;
using FinancialManagerTest.Mocks.Data;
using Microsoft.AspNetCore.Http;

namespace FinancialManagerTest.Tests
{
    public class OperationTypesServiceTest
    {
        [Fact]
        public async Task TestGetAll()
        {
            Assert.Equal(2, (await CreateService().GetAllAsync()).Count());
        }

        [Fact]
        public async Task TestFinancialOperationGetObjectById()
        {
            var service = CreateService();
            var entity = await service.GetAsync(11);
            Assert.NotNull(entity);
            Assert.NotNull(entity);
            Assert.Equal(11, entity.Id);
        }

        [Fact]
        public async Task TestFinancialOperationGetNotExistingObjectById()
        {
            var service = CreateService();
            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.GetAsync(1));
        }

        [Fact]
        public async Task TestDeleteNotExistingObjectObject()
        {
            var service = CreateService();
            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.DeleteAsync(1));
        }

        [Fact]
        public async Task TestDeleteObject()
        {
            var context = new MockFinancialManagerContext();
            var service = new OperationTypeService(context);
            await service.DeleteAsync(12);
            Assert.Single(await service.GetAllAsync());
            Assert.Equal(2, (await new FinancialOperationService(context).GetAllAsync()).Count());
        }

        [Fact]
        public async Task TestUpdateObject()
        {
            var service = CreateService();
            await service.UpdateAsync(11, new OperationType()
            {
                Id = 11,
                Name = "Test",
                IsIncome = true,
            });
            var entity = await service.GetAsync(11);
            Assert.NotNull(entity);
            Assert.Equal(11, entity.Id);
            Assert.True(entity.IsIncome);
            Assert.Equal("Test", entity.Name);
        }

        [Fact]
        public async Task TestUpdateNotExistingObject()
        {
            var service = CreateService();
            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.UpdateAsync(1, new OperationType() { Id = 1 }));
        }

        [Fact]
        public async Task TestUpdateOperationWithAnotherUpdateObjectId()
        {
            var service = CreateService();
            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.UpdateAsync(12, new OperationType() { Id = 2 }));
        }

        [Fact]
        public async Task TestPostObject()
        {
            var service = CreateService();
            await service.AddAsync(new OperationType()
            {
                Id = 1,
                Name = "Test",
                IsIncome = true,
            });
            Assert.Equal(3, (await service.GetAllAsync()).Count());
        }

        private OperationTypeService CreateService()
        {
            return new OperationTypeService(new MockFinancialManagerContext());
        }
    }
}
