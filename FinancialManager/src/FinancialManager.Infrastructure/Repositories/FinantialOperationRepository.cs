using FinancialManager.Shared.Models;
using FinancialManager.Shared.Interfaces.Repositiories;
using FinancialManager.Shared.Exceptions.InfrastructureExceptions;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Infrastructure.Repositiories
{
    public class FinantialOperationRepository : ICRUDRepository<FinancialOperation>
    {
        private readonly FinancialManagerContext _context;
        
        public FinantialOperationRepository(FinancialManagerContext context)
        {
            _context = context;
        }

        public async Task<FinancialOperation> CreateAsync(FinancialOperation entity)
        {
            await _context.FinancialOperations.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FinancialOperation> DeleteAsync(int id)
        {
            var operation = await _context.FinancialOperations.FirstOrDefaultAsync(f => f.Id == id);
            if (operation is null)
            {
                throw new ObjectNotFoundByIdException(typeof(FinancialOperation), id);
            }
            _context.FinancialOperations.Remove(operation);
            await _context.SaveChangesAsync();
            return operation;
        }

        public async Task<IEnumerable<FinancialOperation>> GetAllAsync()
        {
            return await _context.FinancialOperations.ToListAsync();
        }

        public async Task<FinancialOperation> GetByIdAsync(int id)
        {
            var operation = await _context.FinancialOperations.FirstOrDefaultAsync(f => f.Id == id);
            if(operation is null)
            {
                throw new ObjectNotFoundByIdException(typeof(FinancialOperation), id);
            }
            return operation;
        }

        public async Task<FinancialOperation> UpdateAsync(FinancialOperation entity)
        {
            _context.FinancialOperations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
