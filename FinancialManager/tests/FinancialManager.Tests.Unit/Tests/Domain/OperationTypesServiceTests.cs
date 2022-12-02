using AutoMapper;
using FinancialManager.Domain.Services;
using FinancialManager.Shared.Interfaces.Repositiories;
using FinancialManager.Shared.Models;
using FinancialManager.Domain.MapperProfiles;
using FinancialManager.Shared.DTOs;
using FinancialManager.Shared.Exceptions.DomainExceptions;

namespace FinancialManager.Tests.Unit.Tests.Domain
{
    public class OperationTypesServiceTests
    {
        private readonly OperationTypeService _service;
        private readonly ICRUDRepository<OperationType> _repository;
        private readonly IMapper _mapper;
        public OperationTypesServiceTests()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new OperationTypeProfile())).CreateMapper();
            _repository = Substitute.For<ICRUDRepository<OperationType>>();
            _service = new(_repository, _mapper);
        }

        [Fact]
        public async Task GetAll_ReturnsFromRepository()
        {
            var returnValues = _mapper.Map<IEnumerable<OperationType>>(_operations);
            _repository.GetAllAsync().Returns(returnValues);
            var actual = await _service.GetAllAsync();
            actual.Should().BeEquivalentTo(_operations, options => options.ComparingByMembers<FinancialOperationDTO>());

        }

        [Fact]
        public async Task GetById_ReturnsFromRepository()
        {
            var returnValue = _mapper.Map<OperationType>(_operation);
            _repository.GetByIdAsync(1).Returns(returnValue);
            var actual = await _service.GetByIdAsync(1);
            actual.Should().BeEquivalentTo(_operation, options => options.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task Create_ReturnsFromRepository()
        {
            var returnValue = _mapper.Map<OperationType>(_operation);
            _repository.CreateAsync(Arg.Any<OperationType>()).Returns(returnValue);
            var actual = await _service.CreateAsync(_operation);
            actual.Should().BeEquivalentTo(_operation, options => options.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task Delete_ReturnsFromRepository()
        {
            var returnValue = _mapper.Map<OperationType>(_operation);
            _repository.DeleteAsync(1).Returns(returnValue);
            var actual = await _service.DeleteAsync(1);
            actual.Should().BeEquivalentTo(_operation, options => options.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task Update_ReturnsFromRepository()
        {
            var returnValue = _mapper.Map<OperationType>(_operation);
            _repository.UpdateAsync(Arg.Any<OperationType>()).Returns(returnValue);
            var actual = await _service.UpdateAsync(_operation);
            actual.Should().BeEquivalentTo(_operation, options => options.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public void GetById_PassIdAsNull_ThrowException()
        {
            var action = new Func<Task>(async () => await _service.GetByIdAsync(null));
            action.Should().ThrowAsync<NullIdException>().WithMessage("Can`t find object when id is null\nError code: 400");
        }

        [Fact]
        public void Delete_PassIdAsNull_ThrowException()
        {
            var action = new Func<Task>(async () => await _service.DeleteAsync(null));
            action.Should().ThrowAsync<NullIdException>().WithMessage("Can`t find object when id is null\nError code: 400");
        }

        private static readonly IEnumerable<OperationTypeDTO> _operations = new List<OperationTypeDTO>()
        {
            new OperationTypeDTO{ Id = 1, Name = "Name1", IsIncome = false },
            new OperationTypeDTO{ Id = 2, Name = "Name1", IsIncome = false },
            new OperationTypeDTO{ Id = 3, Name = "Name1", IsIncome = false },
            new OperationTypeDTO{ Id = 4, Name = "Name1", IsIncome = false },
        };

        private static readonly OperationTypeDTO _operation = _operations.First();
    }
}
