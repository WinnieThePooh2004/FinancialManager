using FinancialManager.Data;
using FinancialManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Services.CRUDServices
{
    public class OperationTypeService : IService<OperationType>
    {
        private readonly IFinancialManagerContext _context;
        public OperationTypeService(IFinancialManagerContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OperationType entity)
        {
            if (entity is null)
            {
                throw new Exception("Bad request");
            }
            _context.OperationTypes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.OperationTypes.FirstOrDefault(type => type.Id == id);
            if (entity is null)
            {
                throw new Exception("Not found");
            }
            var operationsForDelete = await _context.FinancialOperations.Where(o => o.OperationTypeId == id).ToListAsync();
            foreach (var operation in operationsForDelete) 
            {
                _context.FinancialOperations.Remove(operation);
            }
            _context.OperationTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OperationType>> GetAllAsync()
        {
            return await _context.OperationTypes.ToListAsync();
        }

        public async Task<OperationType> GetAsync(int id)
        {
            var entity = await _context.OperationTypes.FirstOrDefaultAsync(type => type.Id == id);
            if (entity is null)
            {
                throw new Exception("Not found");
            }
            return entity;
        }

        public async Task UpdateAsync(int id, OperationType entity)
        {
            if (id != entity.Id)
            {
                throw new Exception("Bad request");
            }
            var dbEntity = await _context.OperationTypes.FirstOrDefaultAsync(type => type.Id == id);
            if (dbEntity is null)
            {
                throw new Exception("Not found");
            }
            dbEntity.Name = entity.Name;
            dbEntity.IsIncome = entity.IsIncome;
            await _context.SaveChangesAsync();
        }
    }
}
