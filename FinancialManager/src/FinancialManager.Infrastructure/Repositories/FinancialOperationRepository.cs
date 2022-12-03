using FinancialManager.Shared.Models;
using FinancialManager.Shared.Interfaces.Repositiories;
using FinancialManager.Shared.Exceptions.InfrastructureExceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FinancialManager.Shared.Extentions;

#nullable disable

namespace FinancialManager.Infrastructure.Repositiories
{
    public class FinancialOperationRepository : ICRUDRepository<FinancialOperation>
    {
        private readonly FinancialManagerContext _context;
        private readonly ILogger _logger;

        public FinancialOperationRepository(FinancialManagerContext context, ILogger<FinancialOperationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<FinancialOperation> CreateAsync(FinancialOperation entity)
        {
            if (!_context.OperationTypes.Any(o => o.Id == entity.OperationTypeId))
            {
                _logger.LogAndThrow(new InfrastructureExceptions(System.Net.HttpStatusCode.NotFound, $"can`t create operation with OperationTypeId" +
                    $"={entity.OperationTypeId} because it is not any operation types with this id"));
            }
            _context.FinancialOperations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FinancialOperation> DeleteAsync(int id)
        {
            var operation = await _context.FinancialOperations.FirstOrDefaultAsync(f => f.Id == id);
            if (operation is null)
            {
                _logger.LogAndThrow(new ObjectNotFoundByIdException(typeof(FinancialOperation), id));
            }
            _context.FinancialOperations.Remove(operation);
            await _context.SaveChangesAsync();
            return operation;
        }

        public async Task<IEnumerable<FinancialOperation>> GetAllAsync()
        {
            return await _context.FinancialOperations.IgnoreAutoIncludes().ToListAsync();
        }

        public async Task<FinancialOperation> GetByIdAsync(int id)
        {
            var operation = await _context.FinancialOperations.FirstOrDefaultAsync(f => f.Id == id);
            if (operation is null)
            {
                _logger.LogAndThrow(new ObjectNotFoundByIdException(typeof(FinancialOperation), id));
            }
            return operation;
        }

        public async Task<FinancialOperation> UpdateAsync(FinancialOperation entity)
        {
            if (!_context.OperationTypes.Any(o => o.Id == entity.OperationTypeId))
            {
                _logger.LogAndThrow(new InfrastructureExceptions(System.Net.HttpStatusCode.NotFound, $"can`t create operation with OperationTypeId" +
                    $"={entity.OperationTypeId} because it is not any operation types with this id"));
            }
            if (!_context.FinancialOperations.Any(f => f.Id == entity.Id))
            {
                _logger.LogAndThrow(new ObjectNotFoundByIdException(typeof(FinancialOperation), entity.Id));
            }
            _context.FinancialOperations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
#nullable enable
