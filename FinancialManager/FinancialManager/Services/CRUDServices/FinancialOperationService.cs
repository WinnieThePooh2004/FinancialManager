using FinancialManager.Models;
using FinancialManager.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Services.CRUDServices
{
    public class FinancialOperationService : IService<FinancialOperation>
    {
        private IFinancialManagerContext _context;
        public FinancialOperationService(IFinancialManagerContext context)
        {
            _context = context;
        }
        public async Task AddAsync(FinancialOperation? entity)
        {
            if (entity is null || !_context.OperationTypes.Any(type => type.Id == entity.OperationTypeId))
            {
                throw new Exception("No such operation type");
            }

            _context.FinancialOperations.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.FinancialOperations.FirstOrDefault(operation => operation.Id == id);
            if (entity is null)
            {
                throw new Exception("Not found");
            }
            _context.FinancialOperations.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FinancialOperation>> GetAllAsync()
        {
            return await _context.FinancialOperations.ToListAsync();
        }

        public async Task<FinancialOperation> GetAsync(int id)
        {
            var entity = await _context.FinancialOperations.FirstOrDefaultAsync(operation => operation.Id == id);
            if (entity is null)
            {
                throw new Exception("Not found");
            }
            return entity;
        }

        public async Task UpdateAsync(int id, FinancialOperation entity)
        {
            if (id != entity.Id)
            {
                throw new Exception("Edited entity id the same");
            }
            var dbEntity = await _context.FinancialOperations.FirstOrDefaultAsync(operation => operation.Id == id);
            if (dbEntity is null)
            {
                throw new Exception("Not found");
            }
            dbEntity.Description = entity.Description;
            dbEntity.Amount = entity.Amount;
            dbEntity.DateTime = entity.DateTime;
            dbEntity.OperationTypeId = entity.OperationTypeId;
            await _context.SaveChangesAsync();
        }
    }
}
