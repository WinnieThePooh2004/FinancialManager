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
    public class FinancialOperationsServiceTest
    {
        [Fact]
        public async Task TestFinancialOperationsControllerGetAll()
        {
            var service = CreateService();
            Assert.Equal(4, (await service.GetAllAsync()).Count());
        }

        [Fact]
        public async Task TestFinancialOperationGetObjectById()
        {
            var service = CreateService();
            var entity = await service.GetAsync(123);
            Assert.NotNull(entity);
            Assert.NotNull(entity);
            Assert.Equal(123, entity.Id);
        }
        [Fact]
        public async Task TestFinancialOperationGetNotExistingObjectById()
        {
            var service = CreateService();
            await Assert.ThrowsAsync<Exception>(async() => await service.GetAsync(1));
        }

        [Fact]
        public async Task TestDeleteNotExistingObjectObject()
        {
            var service = CreateService();
            await Assert.ThrowsAsync<Exception>(async () => await service.DeleteAsync(1));
        }

        [Fact]
        public async Task TestDeleteObject()
        {
            var context = new MockFinancialManagerContext();
            var service = new FinancialOperationService(context);
            await service.DeleteAsync(123);
            Assert.Equal(3, context.FinancialOperations.Count());
        }

        [Fact]
        public async Task TestUpdateObject()
        {
            var service = new FinancialOperationService(new MockFinancialManagerContext());
            await service.UpdateAsync(123, new FinancialOperation()
            {
                Id = 123,
                Amount = 134,
                DateTime = DateTime.Parse("10.10.2003"),
                OperationTypeId = 12,
                Description = "Rent"
            });
            var entity = await service.GetAsync(123);
            Assert.NotNull(entity);
            Assert.Equal(DateTime.Parse("10.10.2003"), entity.DateTime);
            Assert.Equal(123, entity.Id);
            Assert.Equal(134, entity.Amount);
            Assert.Equal(12, entity.OperationTypeId);
            Assert.Equal("Rent", entity.Description);
        }

        [Fact]
        public async Task TestUpdateNotExistingObject()
        {
            var service = CreateService();
            await Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(2, new FinancialOperation() { Id = 2 }));
        }

        [Fact]
        public async Task TestUpdateOperationWithAnotherUpdateObjectId()
        {
            var service = CreateService();
            await Assert.ThrowsAsync<Exception>(async() => await service.UpdateAsync(123, new FinancialOperation() { Id = 2 }));
        }

        [Fact]
        public async Task TestPostObject()
        {
            var context = new MockFinancialManagerContext();
            var service = new FinancialOperationService(context);
            await service.AddAsync(new FinancialOperation()
            {
                Id = 1,
                Description = "description",
                Amount = 123,
                OperationTypeId = 12
            });
            Assert.Equal(5, context.FinancialOperations.Count());
        }

        [Fact]
        public async Task TestPostObjectWithNotExistingOperationTypeId()
        {
            var service = CreateService();
            await Assert.ThrowsAsync<Exception>(async () => await service.AddAsync(new FinancialOperation() { OperationTypeId = 17 }));
        }

        private FinancialOperationService CreateService()
        {
            return new FinancialOperationService(new MockFinancialManagerContext());
        }
    }
}