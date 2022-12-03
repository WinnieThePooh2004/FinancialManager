using FinancialManager.Infrastructure;
using FinancialManager.Infrastructure.Repositiories;
using FinancialManager.Shared.Exceptions.InfrastructureExceptions;
using FinancialManager.Shared.Models;
using FinancialManager.Tests.Shared.Database;
using FinancialManager.Tests.Shared.Database;
using Microsoft.Extensions.Logging;

namespace FinancialManager.Tests.Unit.Tests.Infrastructure
{
    public class FinancialOperationsRepositoryTests : IDisposable
    {
        private readonly FinancialOperationRepository _repository;
        private readonly FinancialManagerContext _context;
        public FinancialOperationsRepositoryTests()
        {
            _context = DbContextFactory.CreateInMemoryContext();
            _context.Database.EnsureCreated();
            _context.SeedData();
            _repository = new FinancialOperationRepository(_context, Substitute.For<ILogger<FinancialOperationRepository>>());

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Create_ObjectAddedToDb()
        {
            var operation = new FinancialOperation
            {
                Id = 5,
                Amount = 1,
                Description = "abc",
                OperationTypeId = 1,
                DateTime = DateTime.UtcNow,
                OperationType = null
            };
            await _repository.CreateAsync(operation);
            var created = await _repository.GetByIdAsync(5);
            created.Should().BeEquivalentTo(operation, options => options.ComparingByMembers<FinancialOperation>());
        }

        [Fact]
        public async Task Create_NotOperationTypeFound_ShouldThrowException()
        {
            var operation = new FinancialOperation { OperationTypeId = 10 };
            var exception = await new Func<Task>(() => _repository.CreateAsync(operation))
                .Should().ThrowAsync<InfrastructureExceptions>();
        }

        [Fact]
        public async Task Update_ShouldBeUpdatedInDb()
        {
            var operation = await _repository.GetByIdAsync(3);
            operation.Amount = 1;
            operation.DateTime = DateTime.Now;
            await _repository.UpdateAsync(operation);
            var updatedOperation = await _repository.GetByIdAsync(3);
            updatedOperation.Should().BeEquivalentTo(operation, opt => opt.ComparingByMembers<FinancialOperation>());
        }

        [Fact]
        public async Task Update_NotExistingObject_ShouldThrowException()
        {
            var operation = new FinancialOperation
            {
                Id = 10,
                Amount = 1,
                Description = "abc",
                OperationTypeId = 1,
                DateTime = DateTime.UtcNow,
                OperationType = null
            };

            await new Func<Task>(() => _repository.UpdateAsync(operation)).Should()
                .ThrowAsync<ObjectNotFoundByIdException>()
                .WithMessage(new ObjectNotFoundByIdException(typeof(FinancialOperation), 10).Message);
        }

        [Fact]
        public async Task GetById_ReturnsFromDb()
        {
            var expected = DataSeeder.CreateTestFinancialOperations().First();
            var actual = await _repository.GetByIdAsync(1);
            actual.OperationType = null;
            actual.Should().BeEquivalentTo(expected, opt => opt.ComparingByMembers<FinancialOperation>());
        }

        [Fact]
        public async Task GetById_ObjectNotExists_ShouldThrowException()
        {
            await new Func<Task>(() => _repository.GetByIdAsync(10))
                .Should().ThrowAsync<ObjectNotFoundByIdException>()
                .WithMessage(new ObjectNotFoundByIdException(typeof(FinancialOperation), 10).Message);
        }

        [Fact]
        public async Task GetAll_ReturnsFromDb()
        {
            var expected = DataSeeder.CreateTestFinancialOperations();
            var actual = await _repository.GetAllAsync();
            actual.ToList().ForEach(f => f.OperationType = null);
            expected.Should().BeEquivalentTo(actual, opt => opt.ComparingByMembers<FinancialOperation>());
        }

        [Fact]
        public async Task Delete_TotalCountInDbShouldDecrease()
        {
            await _repository.DeleteAsync(1);
            (await _repository.GetAllAsync()).Count().Should().Be(3);
        }

        [Fact]
        public async Task Delete_ObjectNotExists_ShouldThrowException()
        {
            await new Func<Task>(() => _repository.DeleteAsync(10))
                .Should().ThrowAsync<ObjectNotFoundByIdException>()
                .WithMessage(new ObjectNotFoundByIdException(typeof(FinancialOperation), 10).Message);
        }

    }
}
