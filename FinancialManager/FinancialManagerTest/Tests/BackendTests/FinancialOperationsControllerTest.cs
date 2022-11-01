using FinancialManager.Controllers;
using Shared.DTOs.FinancialOperations;
using FinancialManager.Models;
using AutoMapper;
using FinancialManager.Data;
using FinancialManager.MapperProfiles.FinancialOperations;
using Microsoft.AspNetCore.Mvc;
using FinancialManager.Services.CRUDServices;
using FinancialManagerTest.Mocks;
using FinancialManagerTest.Mocks.Data;
using Microsoft.AspNetCore.Http;

namespace FinancialManagerTest.Tests.BackendTests
{
    public class FinancialOperationsControllerTest
    {
        [Fact]
        public async Task TestFinancialOperationsControllerGetAll()
        {
            var controller = CreateController(new FinancialOperationIndexProfile());
            var result = await controller.GetFinacialOperation();
            Assert.NotNull(result.Value);
            Assert.Equal(4, result.Value.Count());
        }

        [Fact]
        public async Task TestFinancialOperationGetObjectById()
        {
            var controller = CreateController(new FinancialOperationDetailsProfile());
            var entity = (await controller.GetFinacialOperation(123)).Value;
            Assert.NotNull(entity);
            Assert.Equal(123, entity.Id);
        }
        [Fact]
        public async Task TestFinancialOperationGetNotExistingObjectById()
        {
            var controller = CreateController(new FinancialOperationDetailsProfile());
            var result = await controller.GetFinacialOperation(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task TestDeleteNotExistingObjectObject()
        {
            var service = CreateController();
            Assert.IsType<NotFoundResult>(await service.DeleteFinacialOperation(1));
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
            var controller = CreateController(new FinancialOperationUpdateProfile(), new FinancialOperationDetailsProfile());
            await controller.PutFinacialOperation(123, new FinancialOperationUpdateDto()
            {
                Id = 123,
                Amount = "134.5",
                DateTime = "10.10.2003",
                OperationTypeId = "12",
                Description = "Rent"
            });
            var entity = (await controller.GetFinacialOperation(123)).Value;
            Assert.NotNull(entity);
            Assert.Equal(DateTime.Parse("10.10.2003").ToString("G"), entity.DateTime);
            Assert.Equal(123, entity.Id);
            Assert.Equal("134.50 UAH", entity.Amount);
            Assert.Equal(12, entity.OperationTypeId);
            Assert.Equal("Rent", entity.Description);
        }

        [Fact]
        public async Task TestUpdateNotExistingObject()
        {
            var controller = CreateController(new FinancialOperationUpdateProfile());
            var result = await controller.PutFinacialOperation(2, new FinancialOperationUpdateDto() { Id = 2 });
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestUpdateOperationWithAnotherUpdateObjectId()
        {
            var controller = CreateController();
            var result = await controller.PutFinacialOperation(123, new FinancialOperationUpdateDto() { Id = 2 });
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestPostObject()
        {
            var context = new MockFinancialManagerContext();
            var controller = CreateController(context, new FinancialOperationCreateProfile());
            var result = await controller.PostFinacialOperation(new FinancialOperationCreateDto()
            {
                Description = "description",
                Amount = "123",
                OperationTypeId = "12"
            });
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(5, context.FinancialOperations.Count());
        }

        [Fact]
        public async Task TestPostObjectWithNotExistingOperationTypeId()
        {
            var controller = CreateController(new FinancialOperationCreateProfile());
            var action = await controller.PostFinacialOperation(new FinancialOperationCreateDto() { OperationTypeId = "4" });
            Assert.IsType<BadRequestResult>(action);
        }

        private FinancialOperationsController CreateController(IFinancialManagerContext context, params Profile[] profiles)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            return new FinancialOperationsController(new FinancialOperationService(context), new Mapper(config));
        }

        private FinancialOperationsController CreateController(params Profile[] profiles)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            return new FinancialOperationsController(new FinancialOperationService(new MockFinancialManagerContext()), new Mapper(config));
        }
    }
}
