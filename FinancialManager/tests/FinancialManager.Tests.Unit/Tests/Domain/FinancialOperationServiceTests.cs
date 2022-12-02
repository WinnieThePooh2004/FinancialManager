using AutoMapper;
using FinancialManager.Domain.Services;
using FinancialManager.Shared.DTOs;
using FinancialManager.Shared.Interfaces.Repositiories;
using FinancialManager.Shared.Models;
using FinancialManager.Domain.MapperProfiles;
using FinancialManager.Shared.Exceptions.DomainExceptions;

namespace FinancialManager.Tests.Unit.Tests.Domain
{
    public class FinancialOperationServiceTests
    {
        private readonly FinancialOperationService _service;
        private readonly ICRUDRepository<FinancialOperation> _repository;
        private readonly IMapper _mapper;
        public FinancialOperationServiceTests()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new FinactialOperationProfile())).CreateMapper();
            _repository = Substitute.For<ICRUDRepository<FinancialOperation>>();
            _service = new(_repository, _mapper);
        }

        [Fact]
        public async Task GetAll_ReturnsFromRepository()
        {
            var returnValues = _mapper.Map<IEnumerable<FinancialOperation>>(_operations);
            _repository.GetAllAsync().Returns(returnValues);
            var actual = await _service.GetAllAsync();
            actual.Should().BeEquivalentTo(_operations, options => options.ComparingByMembers<FinancialOperationDTO>());

        }

        [Fact]
        public async Task GetById_ReturnsFromRepository()
        {
            var returnValue = _mapper.Map<FinancialOperation>(_operation);
            _repository.GetByIdAsync(1).Returns(returnValue);
            var actual = await _service.GetByIdAsync(1);
            actual.Should().BeEquivalentTo(_operation, options => options.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task Create_ReturnsFromRepository()
        {
            var returnValue = _mapper.Map<FinancialOperation>(_operation);
            _repository.CreateAsync(returnValue).Returns(returnValue);
            var actual = await _service.CreateAsync(_operation);
            actual.Should().BeEquivalentTo(_operation, options => options.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task Delete_ReturnsFromRepository()
        {
            var returnValue = _mapper.Map<FinancialOperation>(_operation);
            _repository.DeleteAsync(1).Returns(returnValue);
            var actual = await _service.DeleteAsync(1);
            actual.Should().BeEquivalentTo(_operation, options => options.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task Update_ReturnsFromRepository()
        {
            var returnValue = _mapper.Map<FinancialOperation>(_operation);
            _repository.UpdateAsync(Arg.Any<FinancialOperation>()).Returns(returnValue);
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

        private static readonly IEnumerable<FinancialOperationDTO> _operations = new List<FinancialOperationDTO>()
        {
            new FinancialOperationDTO{ Id = 1, Amount = 100, DateTime = DateTime.Now, Description = "abc", OperationTypeId = 11 },
            new FinancialOperationDTO{ Id = 2, Amount = 100, DateTime = DateTime.Now, Description = "bbc", OperationTypeId = 11 },
            new FinancialOperationDTO{ Id = 3, Amount = 100, DateTime = DateTime.Now, Description = "cbc", OperationTypeId = 11 },
            new FinancialOperationDTO{ Id = 4, Amount = 100, DateTime = DateTime.Now, Description = "dbc", OperationTypeId = 11 },
        };

        private static readonly FinancialOperationDTO _operation = _operations.First();
    }
}
