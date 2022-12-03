using FinancialManager.Api.Controllers;
using FinancialManager.Domain.Validatiors;
using FinancialManager.Shared.DTOs;
using FinancialManager.Shared.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.Tests.Unit.Tests.Api
{
    public class OperationTypesControllerTests
    {
        private readonly ICRUDService<OperationTypeDTO> _service;
        private readonly OperationTypesController _controller;
        public OperationTypesControllerTests() 
        {
            _service = Substitute.For<ICRUDService<OperationTypeDTO>>();
            _controller = new OperationTypesController(_service);
        }

        [Fact]
        public async Task GetAll_ReturnsFromService()
        {
            var expected = new List<OperationTypeDTO>
            {
                new OperationTypeDTO{ Id = 1, IsIncome = true, Name = "salary" },
                new OperationTypeDTO{ Id = 2, IsIncome = false, Name = "rent" },
            };
            _service.GetAllAsync().Returns(expected);
            var actual = await _controller.GetAll();
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(expected);
        }

        [Fact]
        public async Task Create_ReturnsFromService()
        {
            var expected = new OperationTypeDTO { Id = 1, IsIncome = true, Name = "salary" };
            _service.CreateAsync(expected).Returns(expected);
            var actual = await _controller.Post(expected);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(expected);
        }

        [Fact]
        public async Task Delete_ReturnsFromService()
        {
            var expected = new OperationTypeDTO { Id = 1, IsIncome = true, Name = "salary" };
            _service.DeleteAsync(1).Returns(expected);
            var actual = await _controller.Delete(1);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(expected);
        }

        [Fact]
        public async Task Update_ReturnsFromService()
        {
            var expected = new OperationTypeDTO { Id = 1, IsIncome = true, Name = "salary" };
            _service.UpdateAsync(expected).Returns(expected);
            var actual = await _controller.Put(expected);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(expected);
        }

        [Fact]
        public async Task GetById_ReturnsFromService()
        {
            var expected = new OperationTypeDTO { Id = 1, IsIncome = true, Name = "salary" };
            _service.GetByIdAsync(1).Returns(expected);
            var actual = await _controller.Get(1);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(expected);
        }

        [Fact]
        public async Task PassedInvalidObject_ShouldHaveValidationError()
        {
            var invalidItem = new OperationTypeDTO { Name = "" };
            var validator = new OperationTypeDTOValidator();
            var errors = await validator.ValidateAsync(invalidItem);
            var errorList = errors.Errors.Select(error => error.ErrorMessage).ToList();
            var expecterErrors = new List<string>
            {
                "Please, enter name",
            };
            errorList.Should().BeEquivalentTo(expecterErrors);
        }

    }
}
