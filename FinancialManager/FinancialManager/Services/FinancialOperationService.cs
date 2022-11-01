using FinancialManager.Models;
using FinancialManager.Data;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Services.CRUDServices;

namespace FinancialManager.Services
{
    public class FinancialOperationService : IService<FinancialOperation>
    {
        private FinancialManagerContext _context;
        public FinancialOperationService(FinancialManagerContext context)
        {
            _context = context;
        }
        public async Task AddAsync(FinancialOperation? entity)
        {
            if (entity is null || !_context.OperationTypes.All(type => type.Id == entity.OperationTypeId))
            {
                throw new Exception("Bad request");
            }

            _context.FinancialOperations.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.FinancialOperations.FirstOrDefault(x => x.Id == id);
            if (entity is null)
            {
                throw new Exception("Not founded");
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
            if(entity is null)
            {
                throw new Exception("Not found");
            }
            return entity;
        }

        public async Task UpdateAsync(int id, FinancialOperation entity)
        {
            if (id != entity.Id)
            {
                throw new Exception("Bad request");
            }
            var dbEntity = await _context.FinancialOperations.FirstOrDefaultAsync(operation => operation.Id == id);
            if (dbEntity is null)
            {
                throw new Exception("Not founded");
            }
            dbEntity.Description = entity.Description;
            dbEntity.Amount = entity.Amount;
            dbEntity.DateTime = entity.DateTime;
            dbEntity.OperationTypeId = entity.OperationTypeId;
            await _context.SaveChangesAsync();
        }
    }
}
