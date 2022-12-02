using FinancialManager.Shared.Exceptions.InfrastructureExceptions;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Shared.Models;
using FinancialManager.Shared.Interfaces.Repositiories;

namespace FinancialManager.Infrastructure.Repositiories
{

    public class OperationTypeRepository : ICRUDRepository<OperationType>
    {
        private readonly FinancialManagerContext _context;

        public OperationTypeRepository(FinancialManagerContext context) 
        { 
            _context = context;
        }

        public async Task<OperationType> CreateAsync(OperationType entity)
        {
            await _context.OperationTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<OperationType> DeleteAsync(int id)
        {
            var type = await _context.OperationTypes.FirstOrDefaultAsync(f => f.Id == id);
            if (type is null)
            {
                throw new ObjectNotFoundByIdException(typeof(OperationType), id);
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
                throw new ObjectNotFoundByIdException(typeof(OperationType), id);
            }
            return type;
        }

        public async Task<OperationType> UpdateAsync(OperationType entity)
        {
            if (!_context.OperationTypes.Any(type => type.Id == entity.Id))
            {
                throw new Exception("Not found");
            }
            _context.OperationTypes.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
