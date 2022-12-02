using FinancialManager.Controllers;
using FinancialManager.Domain.Validatiors;
using FinancialManager.Shared.DTOs;
using FinancialManager.Shared.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.Tests.Unit.Tests.Api
{
    public class FinancialOperationControllerTests
    {
        private readonly ICRUDService<FinancialOperationDTO> _service;
        private readonly FinancialOperationsController _controller;
        public FinancialOperationControllerTests()
        {
            _service = Substitute.For<ICRUDService<FinancialOperationDTO>>();
            _controller = new FinancialOperationsController(_service);
        }

        [Fact]
        public async Task GetAll_ReturnsFromService()
        {
            var expected = new List<FinancialOperationDTO>
            {
                new FinancialOperationDTO{ Id = 1, Amount = 100, DateTime = DateTime.Now, Description = "abc", OperationTypeId = 1 },
                new FinancialOperationDTO{ Id = 2, Amount = 1000, DateTime = DateTime.Now, Description = "dbc", OperationTypeId = 2 }
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
            var expected = new FinancialOperationDTO { Id = 1, Amount = 100, DateTime = DateTime.Now, Description = "abc", OperationTypeId = 1 };
            _service.CreateAsync(expected).Returns(expected);
            var actual = await _controller.Post(expected);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(expected);
        }

        [Fact]
        public async Task Delete_ReturnsFromService()
        {
            var expected = new FinancialOperationDTO { Id = 1, Amount = 100, DateTime = DateTime.Now, Description = "abc", OperationTypeId = 1 };
            _service.DeleteAsync(1).Returns(expected);
            var actual = await _controller.DeleteFinacialOperation(1);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(expected);
        }

        [Fact]
        public async Task Update_ReturnsFromService()
        {
            var expected = new FinancialOperationDTO { Id = 1, Amount = 100, DateTime = DateTime.Now, Description = "abc", OperationTypeId = 1 };
            _service.UpdateAsync(expected).Returns(expected);
            var actual = await _controller.Put(expected);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(expected);
        }

        [Fact]
        public async Task GetById_ReturnsFromService()
        {
            var expected = new FinancialOperationDTO { Id = 1, Amount = 100, DateTime = DateTime.Now, Description = "abc", OperationTypeId = 1 };
            _service.GetByIdAsync(1).Returns(expected);
            var actual = await _controller.Get(1);
            actual.Should().NotBeNull();
            actual.Should().BeOfType<OkObjectResult>();
            actual.As<OkObjectResult>().Value.Should().Be(expected);
        }

        [Fact]
        public async Task PassedInvalidObject_ShouldHaveValidationError()
        {
            var invalidItem = new FinancialOperationDTO { Amount = -1, OperationTypeId = 0 };
            var validator = new FinancialOperationDTOValidator();
            var errors = await validator.ValidateAsync(invalidItem);
            var errorList = errors.Errors.Select(error => error.ErrorMessage).ToList();
            var expecterErrors = new List<string>
            {
                "You can`t get or spend less than 0",
                "Plese, select operation type"
            };
            errorList.Should().BeEquivalentTo(expecterErrors);
        }
    }
}
