using FinancialManager.Controllers;
using Shared.DTOs.OperationTypes;
using FinancialManager.Models;
using AutoMapper;
using FinancialManager.MapperProfiles.OperationTypes;
using Microsoft.AspNetCore.Mvc;
using FinancialManager.Services.CRUDServices;
using FinancialManagerTest.Mocks;
using FinancialManagerTest.Mocks.Data;
using Microsoft.AspNetCore.Http;
using FinancialManager.Data;

namespace FinancialManagerTest.Tests.BackendTests
{
    public class OperationTypesControllerTest
    {
        [Fact]
        public async Task TestGetAll()
        {
            var response = await CreateController(new OperationTypeIndexProfile()).GetOperationType();
            Assert.NotNull(response.Value);
            Assert.Equal(2, response.Value.Count());
        }

        [Fact]
        public async Task TestFinancialOperationGetObjectById()
        {
            var controller = CreateController(new OperationTypeDetailsProfile());
            var response = await controller.GetOperationType(11);
            var entity = response.Value;
            Assert.NotNull(entity);
            Assert.Equal(11, entity.Id);
        }

        [Fact]
        public async Task TestFinancialOperationGetNotExistingObjectById()
        {
            var controller = CreateController(new OperationTypeDetailsProfile());
            Assert.IsType<NotFoundResult>((await controller.GetOperationType(1)).Result);
        }

        [Fact]
        public async Task TestDeleteNotExistingObjectObject()
        {
            var controller = CreateController();
            Assert.IsType<NotFoundResult>(await controller.DeleteOperationType(1));
        }

        [Fact]
        public async Task TestDeleteObject()
        {
            var context = new MockFinancialManagerContext();
            var service = CreateController(context);
            await service.DeleteOperationType(12);
            Assert.Equal(1, context.OperationTypes.Count());
            Assert.Equal(2, context.FinancialOperations.Count());
        }

        [Fact]
        public async Task TestUpdateObject()
        {
            var controller = CreateController(new OperationTypeUpdateProfile(), new OperationTypeDetailsProfile());
            await controller.PutOperationType(11, new OperationTypeUpdateDto()
            {
                Id = 11,
                Name = "Test",
                IsIncome = true,
            });
            var response = await controller.GetOperationType(11);
            var entity = response.Value;
            Assert.NotNull(entity);
            Assert.Equal(11, entity.Id);
            Assert.True(entity.IsIncome);
            Assert.Equal("Test", entity.Name);
        }

        [Fact]
        public async Task TestUpdateNotExistingObject()
        {
            var controller = CreateController(new OperationTypeUpdateProfile());
            Assert.IsType<NotFoundResult>(await controller.PutOperationType(1, new OperationTypeUpdateDto() { Id = 1 }));
        }

        [Fact]
        public async Task TestUpdateOperationWithAnotherUpdateObjectId()
        {
            var controller = CreateController(new OperationTypeUpdateProfile());
            Assert.IsType<BadRequestResult>(await controller.PutOperationType(12, new OperationTypeUpdateDto() { Id = 2 }));
        }

        [Fact]
        public async Task TestPostObject()
        {
            var context = new MockFinancialManagerContext();
            var controller = CreateController(context, new OperationTypeCreateProfile());
            await controller.PostOperationType(new OperationTypeCreateDto()
            {
                Name = "Test",
                IsIncome = true,
            });
            Assert.Equal(3, context.OperationTypes.Count());
        }

        private OperationTypesController CreateController(IFinancialManagerContext context, params Profile[] profiles)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            return new OperationTypesController(new OperationTypeService(context), new Mapper(config));
        }

        private OperationTypesController CreateController(params Profile[] profiles)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            return new OperationTypesController(new OperationTypeService(new MockFinancialManagerContext()), new Mapper(config));
        }

    }
}
