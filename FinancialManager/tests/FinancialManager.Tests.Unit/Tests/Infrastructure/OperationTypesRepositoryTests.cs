using FinancialManager.Infrastructure.Repositiories;
using FinancialManager.Infrastructure;
using FinancialManager.Shared.Exceptions.InfrastructureExceptions;
using FinancialManager.Shared.Models;
using FinancialManager.Tests.Shared.Database;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Tests.Unit.Tests.Infrastructure
{
    public class OperationTypesRepositoryTests : IDisposable
    {
        private readonly OperationTypeRepository _repository;
        private readonly FinancialManagerContext _context;
        public OperationTypesRepositoryTests()
        {
            _context = DbContextFactory.CreateInMemoryContext();
            _context.Database.EnsureCreated();
            _context.SeedData();
            _repository = new (_context, Substitute.For<ILogger<OperationTypeRepository>>());

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Create_ObjectAddedToDb()
        {
            var operation = new OperationType
            {
                Name = "a",
                IsIncome = true,
            };
            await _repository.CreateAsync(operation);
            var created = await _repository.GetByIdAsync(3);
            created.Should().BeEquivalentTo(operation, options => options.ComparingByMembers<OperationType>());
        }

        [Fact]
        public async Task Update_ShouldBeUpdatedInDb()
        {
            var operation = await _repository.GetByIdAsync(2);
            operation.Name = "jsw";
            operation.IsIncome = !operation.IsIncome;
            await _repository.UpdateAsync(operation);
            var updatedOperation = await _repository.GetByIdAsync(2);
            updatedOperation.Should().BeEquivalentTo(operation, opt => opt.ComparingByMembers<OperationType>());
        }

        [Fact]
        public async Task Update_NotExistingObject_ShouldThrowException()
        {
            var operation = new OperationType
            {
                Id = 30,
                Name = "a",
                IsIncome = true,
            };
            await new Func<Task>(() => _repository.UpdateAsync(operation)).Should()
                .ThrowAsync<ObjectNotFoundByIdException>()
                .WithMessage(new ObjectNotFoundByIdException(typeof(OperationType), 30).Message);
        }

        [Fact]
        public async Task GetById_ReturnsFromDb()
        {
            var expected = DataSeeder.CreateTestOperationTypes().First();
            var actual = await _repository.GetByIdAsync(1);
            actual.Operations = null;
            actual.Should().BeEquivalentTo(expected, opt => opt.ComparingByMembers<OperationType>());
        }

        [Fact]
        public async Task GetById_ObjectNotExists_ShouldThrowException()
        {
            await new Func<Task>(() => _repository.GetByIdAsync(10))
                .Should().ThrowAsync<ObjectNotFoundByIdException>()
                .WithMessage(new ObjectNotFoundByIdException(typeof(OperationType), 10).Message);
        }

        [Fact]
        public async Task GetAll_ReturnsFromDb()
        {
            var expected = DataSeeder.CreateTestOperationTypes();
            var actual = await _repository.GetAllAsync();
            actual.ToList().ForEach(f => f.Operations = null);
            expected.Should().BeEquivalentTo(actual, opt => opt.ComparingByMembers<OperationType>());
        }

        [Fact]
        public async Task Delete_TotalCountInDbShouldDecrease()
        {
            await _repository.DeleteAsync(1);
            (await _repository.GetAllAsync()).Count().Should().Be(1);
            (await _context.FinancialOperations.CountAsync()).Should().Be(2);
        }

        [Fact]
        public async Task Delete_ObjectNotExists_ShouldThrowException()
        {
            await new Func<Task>(() => _repository.DeleteAsync(10))
                .Should().ThrowAsync<ObjectNotFoundByIdException>()
                .WithMessage(new ObjectNotFoundByIdException(typeof(OperationType), 10).Message);
        }

    }
}
