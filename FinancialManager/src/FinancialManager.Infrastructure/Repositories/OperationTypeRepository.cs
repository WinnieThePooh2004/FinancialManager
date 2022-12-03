using FinancialManager.Shared.Exceptions.InfrastructureExceptions;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Shared.Models;
using FinancialManager.Shared.Interfaces.Repositiories;
using Microsoft.Extensions.Logging;
using FinancialManager.Shared.Extentions;

#nullable disable

namespace FinancialManager.Infrastructure.Repositiories
{

    public class OperationTypeRepository : ICRUDRepository<OperationType>
    {
        private readonly FinancialManagerContext _context;
        private readonly ILogger _logger;
        public OperationTypeRepository(FinancialManagerContext context, ILogger<OperationTypeRepository> logger) 
        { 
            _context = context;
            _logger = logger;
        }

        public async Task<OperationType> CreateAsync(OperationType entity)
        {
            await _context.OperationTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<OperationType> DeleteAsync(int id)
        {
            var type = await _context.OperationTypes
                .Include(o => o.Operations)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (type is null)
            {
                _logger.LogAndThrow(new ObjectNotFoundByIdException(typeof(OperationType), id));
            }
            _context.OperationTypes.Remove(type);
            await _context.SaveChangesAsync();
            return type;
        }

        public async Task<IEnumerable<OperationType>> GetAllAsync()
        {
            return await _context.OperationTypes.ToListAsync();
        }

        public async Task<OperationType> GetByIdAsync(int id)
        {
            var type = await _context.OperationTypes.FirstOrDefaultAsync(f => f.Id == id);
            if (type is null) 
            {
                _logger.LogAndThrow(new ObjectNotFoundByIdException(typeof(OperationType), id));
            }
            return type;
        }

        public async Task<OperationType> UpdateAsync(OperationType entity)
        {
            if (!_context.OperationTypes.Any(type => type.Id == entity.Id))
            {
                _logger.LogAndThrow(new ObjectNotFoundByIdException(typeof(OperationType), entity.Id));
            }
            _context.OperationTypes.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

#nullable enable